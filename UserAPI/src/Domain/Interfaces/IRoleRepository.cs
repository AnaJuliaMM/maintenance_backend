using UserAPI.Domain.Entities;

namespace UserAPI.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role?> GetById(int id);
        Task Add(Role role);
        Task Update(Role role);
        Task Delete(int id);
    }
}
