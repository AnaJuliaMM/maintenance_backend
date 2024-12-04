using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.API.DTOs;
using WarehouseAPI.Application.Interfaces;

namespace WarehouseAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user:admin,user:editor, user:viewer")]
        public async Task<ActionResult<ItemDTO?>> GetById(int id)
        {
            try
            {
                ItemDTO? item = await _itemService.GetById(id);
                return Ok(item);
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
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAll()
        {
            try
            {
                IEnumerable<ItemDTO> item = await _itemService.GetAll();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "user:editor, user:admin")]
        public async Task<ActionResult> Create([FromBody] ItemDTO itemDTO)
        {
            try
            {
                await _itemService.Add(itemDTO);
                return CreatedAtAction(nameof(GetById), new { id = itemDTO.Id }, itemDTO);
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
        public async Task<ActionResult> Update(int id, [FromBody] ItemDTO itemDTO)
        {
            try
            {
                await _itemService.Update(id, itemDTO);
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
                await _itemService.Delete(id);
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
