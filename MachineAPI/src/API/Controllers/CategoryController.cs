using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;

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
    public async Task<ActionResult<CategoryDTO?>> GetById(int id)
    {
        CategoryDTO? category = await _categoryService.GetById(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
    {
        IEnumerable<CategoryDTO> categories = await _categoryService.GetAll();
        return Ok(categories);
    }
}

}