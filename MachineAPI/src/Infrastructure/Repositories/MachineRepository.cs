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
            return await _context.Machines
                .Include(m => m.Category)
                .Include(m => m.Location)
                .ToListAsync();
        }

        public async Task<Machine?> GetById(int id)
        {
            return await _context.Machines
                .Include(m => m.Category)
                .Include(m => m.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
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

        public async Task AddLocationToMachine(int machineId, Location location)
        {   
            // Find existing machine row
            Machine? machine = await _context.Machines.FindAsync(machineId);
            // Associate machine location fk witk location pk
            machine.LocationId = location.Id;
            await _context.SaveChangesAsync();
        }

        public async Task AddCategoryToMachine(int machineId, Category category)
        {   
            // Find existing machine row
            Machine? machine = await _context.Machines.FindAsync(machineId);
            // Associate machine category fk witk category pk
            machine.CategoryId = category.Id;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLocationFromMachine(int machineId)
        {
            // Find existing machine row
            Machine? machine = await _context.Machines.FindAsync(machineId);
            // Remove location pk 
            machine.LocationId = null;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCategoryFromMachine(int machineId)
        {
            // Find existing machine row
            Machine? machine = await _context.Machines.FindAsync(machineId);
            // Remove category pk 
            machine.CategoryId = null;
            await _context.SaveChangesAsync();
        }

    }


}

    

