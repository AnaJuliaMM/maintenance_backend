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
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido");
            }

            Category? category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrado.");
            }

            return category != null ? _mapper.Map<CategoryDTO>(category) : null;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO), "Nenhum dado foi recebido");
            }

            Category category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Add(category);
        }

        public async Task Update(int id, CategoryDTO categoryDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido");
            }

            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO), "Nenhum dado foi recebido");
            }

            Category? category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrado.");
            }

            _mapper.Map(categoryDTO, category);

            await _categoryRepository.Update(category);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido");
            }

            Category? category = await _categoryRepository.GetById(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrado.");
            }

            await _categoryRepository.Delete(id);
        }
    }
}
