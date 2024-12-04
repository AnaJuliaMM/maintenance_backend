using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user:admin,user:editor, user:viewer")]
        public async Task<ActionResult<LocationDTO?>> GetById(int id)
        {
            try
            {
                LocationDTO? location = await _locationService.GetById(id);
                return Ok(location);
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
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAll()
        {
            try
            {
                IEnumerable<LocationDTO> locations = await _locationService.GetAll();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "user:editor, user:admin")]
        public async Task<ActionResult> Create([FromBody] LocationDTO locationDTO)
        {
            try
            {
                LocationDTO createdLocationDTO = await _locationService.Add(locationDTO);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdLocationDTO.Id },
                    createdLocationDTO
                );
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
        [Authorize(Roles = "user:editor, user:admin")]
        public async Task<ActionResult> Update(int id, [FromBody] LocationDTO locationDTO)
        {
            try
            {
                await _locationService.Update(id, locationDTO);
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
                await _locationService.Delete(id);
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
