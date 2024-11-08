using AutoMapper;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;
using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Services
{
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDTO?> GetById(int id)
    {
        Category? category = await _categoryRepository.GetById(id);
        return category != null ? _mapper.Map<CategoryDTO>(category) : null;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAll()
    {
        IEnumerable<Category> categories = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }
}
}