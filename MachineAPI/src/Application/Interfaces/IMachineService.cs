using MachineAPI.API.DTOs;

namespace MachineAPI.Application.Interfaces
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineDTO>> GetAll();
        Task<MachineDTO?> GetById(int id);
        Task<CreateUpdateMachineDTO> Add(CreateUpdateMachineDTO machineDTO);
        Task Update(int id, CreateUpdateMachineDTO machineDTO);
        Task Delete(int id);
    }
}
