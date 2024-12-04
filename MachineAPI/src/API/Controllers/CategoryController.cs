using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MachineAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "user:admin,user:editor, user:viewer")]
        public async Task<ActionResult<CategoryDTO?>> GetById(int id)
        {
            try
            {
                CategoryDTO? category = await _categoryService.GetById(id);
                return Ok(category);
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            try
            {
                IEnumerable<CategoryDTO> categories = await _categoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "user:editor, user:admin")]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                CategoryDTO createdCategoryDTO = await _categoryService.Add(categoryDTO);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdCategoryDTO.Id },
                    createdCategoryDTO
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
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                await _categoryService.Update(id, categoryDTO);
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
                await _categoryService.Delete(id);
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
