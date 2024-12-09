using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.API.DTOs;
using UserAPI.Application.Interfaces;

namespace UserAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user:admin")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                UserDTO? user = await _userService.GetById(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro de argumento: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<UserDTO> users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateUserDTO userDTO)
        {
            try
            {
                CreateUpdateUserDTO createdUserDTO = await _userService.Add(userDTO);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdUserDTO.Id },
                    createdUserDTO
                );
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Argumento inváldo: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                if (
                    ex.InnerException is Npgsql.PostgresException foreignKeyEx
                    && foreignKeyEx.SqlState == "23503"
                )
                    return BadRequest(
                        "O Role especificado não existe. Verifique o RoleId e tente novamente."
                    );
                else if (
                    ex.InnerException is Npgsql.PostgresException uniqueConstraintEx
                    && uniqueConstraintEx.SqlState == "23505"
                )
                    return BadRequest("O username já existe.");
                else
                    return BadRequest("Erro ao persistir no banco de dados");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateUpdateUserDTO userDTO)
        {
            try
            {
                await _userService.Update(id, userDTO);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Erro de argumento: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro de argumento: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro de argumento: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
