using Microsoft.EntityFrameworkCore;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Infrastructure.Data
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

        public async Task<Role> Add(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task Update(Role role)
        {
            _context.Roles.Update(role);
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
