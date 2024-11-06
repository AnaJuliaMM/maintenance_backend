using Microsoft.EntityFrameworkCore;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;

namespace MachineAPI.Infrastructure.Data
{
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationDbContext _context;

        public MachineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Machine>> GetAll()
        {
            return await _context.Machines.ToListAsync();
        }

        public async Task<Machine?> GetById(int id)
        {
            return await _context.Machines.FindAsync(id);
        }

        public async Task Add(Machine machine)
        {
            await _context.Machines.AddAsync(machine);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Machine machine)
        {
            _context.Machines.Update(machine);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Machine? machine = await _context.Machines.FindAsync(id);
            if (machine != null)
            {
                _context.Machines.Remove(machine);
                await _context.SaveChangesAsync();
            }
        }
    }
}
