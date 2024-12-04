using UserAPI.Domain.Entities;

namespace UserAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> FindByUsername(string username);
        Task<User> Add(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
