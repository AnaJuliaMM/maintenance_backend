using Microsoft.EntityFrameworkCore;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;
using UserAPI.Infrastructure.Data;

namespace RoleAPI.Infrastructure.Data
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task Add(Role Role)
        {
            await _context.Roles.AddAsync(Role);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Role Role)
        {
            _context.Roles.Update(Role);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Role = await _context.Roles.FindAsync(id);
            if (Role != null)
            {
                _context.Roles.Remove(Role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
