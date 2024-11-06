using MachineAPI.Domain.Entities;

namespace MachineAPI.Domain.Interfaces
{
    public interface IMachineRepository
    {
        Task<IEnumerable<Machine>> GetAll();
        Task<Machine?> GetById(int id);
        Task Add(Machine machine);
        Task Update(Machine machine);
        Task Delete(int id);
    }
}
