using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Interfaces
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineDTO>> GetAll();
        Task<MachineDTO> GetById(int id);
        Task Add(MachineDTO machineDTO);
        Task Update(int id, MachineDTO machineDTO);
        Task Delete(int id);
    }
}
