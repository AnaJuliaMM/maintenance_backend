using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<MachineDTO>> GetById(int id)
        {
            MachineDTO? machine = await _machineService.GetById(id);
            if (machine == null)
            {
                return NotFound("Machine ID does not exist");
            }
            return Ok(machine);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetAll()
        {
            IEnumerable<MachineDTO> machines = await _machineService.GetAll();
            return Ok(machines);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUpdateMachineDTO MachineDTO)
        {
            await _machineService.Add(MachineDTO);
            return CreatedAtAction(nameof(GetById), new { id = MachineDTO.Id }, MachineDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateUpdateMachineDTO MachineDTO)
        {
            if (id != MachineDTO.Id)
            {
                return BadRequest();
            }

            await _machineService.Update(id, MachineDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _machineService.Delete(id);
            return NoContent();
        }
    }
}
