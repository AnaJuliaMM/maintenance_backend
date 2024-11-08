using MachineAPI.Domain.Entities;

namespace MachineAPI.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location?> GetById(int id);
        Task<Location?> GetByName(string name);
        Task Add(Location location);
        Task Update(Location location);
        Task Delete(int id);
    }
}
