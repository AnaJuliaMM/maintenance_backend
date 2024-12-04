using UserAPI.API.DTOs;

namespace UserAPI.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAll();
        Task<RoleDTO?> GetById(int id);
        Task<RoleDTO> Add(RoleDTO roleDTO);
        Task Update(int id, RoleDTO roleDTO);
        Task Delete(int id);
    }
}
