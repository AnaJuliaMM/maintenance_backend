using UserAPI.API.DTOs;

namespace UserAPI.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAll();
        Task<RoleDTO?> GetById(int id);
        Task Add(RoleDTO userDTO);
        Task Update(int id, RoleDTO userDTO);
        Task Delete(int id);
    }
}
