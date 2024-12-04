using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MachineAPI.Infrastructure.Data
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location?> GetById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location?> GetByName(string locationName)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.Name == locationName);
        }

        public async Task<Location> Add(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task Update(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Location? location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}
