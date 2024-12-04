using MachineAPI.Domain.Entities;

namespace MachineAPI.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task<Category?> GetByName(string categoryName);
        Task<Category> Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
    }
}
