using MachineAPI.Domain.Entities;

namespace MachineAPI.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
    }
}
