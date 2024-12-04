using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAPI.API.DTOs;
using UserAPI.Application.Interfaces;

namespace UserAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "user:admin")]
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
                await _userService.Add(userDTO);
                return CreatedAtAction(nameof(GetById), new { id = userDTO.Username }, userDTO);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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
