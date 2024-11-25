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
        public async Task<ActionResult<LocationDTO?>> GetById(int id)
        {
            LocationDTO? location = await _locationService.GetById(id);
            if (location == null)
            {
                return NotFound("Location ID does not exist");
            }
            return Ok(location);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAll()
        {
            IEnumerable<LocationDTO> locations = await _locationService.GetAll();
            return Ok(locations);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LocationDTO locationDTO)
        {
            await _locationService.Add(locationDTO);
            return CreatedAtAction(nameof(GetById), new { id = locationDTO.Id }, locationDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LocationDTO locationDTO)
        {
            if (id != locationDTO.Id)
            {
                return BadRequest();
            }

            await _locationService.Update(id, locationDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _locationService.Delete(id);
            return NoContent();
        }
    }
}
