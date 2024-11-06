using MachineAPI.Domain.Entities;

namespace MachineAPI.Domain.Interfaces
{
    public interface IMachineLocationRepository
    {
        Task<IEnumerable<MachineLocation>> GetAll();
        Task<MachineLocation> GetById(int id);
        Task Add(MachineLocation machineLocation);
        Task Update(MachineLocation machineLocation);
        Task Delete(int id);
    }
}
