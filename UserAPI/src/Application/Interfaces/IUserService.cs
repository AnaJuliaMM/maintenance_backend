using UserAPI.API.DTOs;

namespace UserAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO?> GetById(int id);
        Task<CreateUpdateUserDTO> Add(CreateUpdateUserDTO userDTO);
        Task Update(int id, CreateUpdateUserDTO userDTO);
        Task Delete(int id);
    }
}
