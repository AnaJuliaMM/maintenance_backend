using UserAPI.API.DTOs;

namespace UserAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO?> GetById(int id);
        Task Add(UserDTO userDTO);
        Task Update(int id, UserDTO userDTO);
        Task Delete(int id);
    }
}
