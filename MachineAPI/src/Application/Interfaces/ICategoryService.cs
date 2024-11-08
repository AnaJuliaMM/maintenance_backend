using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Interfaces
{
public interface ICategoryService
{
    Task<CategoryDTO?> GetById(int id);
    Task<IEnumerable<CategoryDTO>> GetAll();
}
}
