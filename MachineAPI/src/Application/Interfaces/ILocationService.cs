using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetAll();
        Task<LocationDTO?> GetById(int id);
        Task<LocationDTO> Add(LocationDTO locationDTO);
        Task Update(int id, LocationDTO locationDTO);
        Task Delete(int id);
    }
}
