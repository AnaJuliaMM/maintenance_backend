using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;

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
            return NotFound();
        }
        return Ok(location);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAll()
    {
        IEnumerable<LocationDTO> locations = await _locationService.GetAll();
        return Ok(locations);
    }
}

}