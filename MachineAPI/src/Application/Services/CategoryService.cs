using AutoMapper;
using MachineAPI.API.DTOs;
using MachineAPI.Application.Interfaces;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;

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

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetById(int id)
        {
            Category? category = await _categoryRepository.GetById(id);
            return category != null ? _mapper.Map<CategoryDTO>(category) : null;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Add(category);
        }

        public async Task Update(int id, CategoryDTO categoryDTO)
        {
            Category category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            _mapper.Map(categoryDTO, category);

            await _categoryRepository.Update(category);
        }

        public async Task Delete(int id)
        {
            await _categoryRepository.Delete(id);
        }
    }
}
