using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user:admin,user:editor, user:viewer")]
        public async Task<ActionResult<MachineDTO>> GetById(int id)
        {
            try
            {
                MachineDTO? machine = await _machineService.GetById(id);
                return Ok(machine);
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
        [Authorize(Roles = "user:admin,user:editor, user:viewer")]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetAll()
        {
            try
            {
                IEnumerable<MachineDTO> machines = await _machineService.GetAll();
                return Ok(machines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "user:editor, user:admin")]
        public async Task<ActionResult> Create([FromBody] CreateUpdateMachineDTO MachineDTO)
        {
            try
            {
                CreateUpdateMachineDTO createdMachineDTO = await _machineService.Add(MachineDTO);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdMachineDTO.Id },
                    createdMachineDTO
                );
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is Npgsql.PostgresException postgresEx
                    && postgresEx.SqlState == "23503"
                )
            {
                return BadRequest(
                    "A Categoria ou a Localização especificada não existe. Verifique o CategoryID ou LocationID e tente novamente."
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "user:editor, user:admin")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateUpdateMachineDTO MachineDTO)
        {
            try
            {
                await _machineService.Update(id, MachineDTO);
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
        [Authorize(Roles = "user:admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _machineService.Delete(id);
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
