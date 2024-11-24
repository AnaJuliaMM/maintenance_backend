using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO?> GetById(int id);
        Task Add(CategoryDTO categoryDTO);
        Task Update(int id, CategoryDTO categoryDTO);
        Task Delete(int id);
    }
}
