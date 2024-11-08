using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Interfaces
{
    public interface ILocationService
    {
        Task<LocationDTO?> GetById(int id);
        Task<IEnumerable<LocationDTO>> GetAll();
    }
}
