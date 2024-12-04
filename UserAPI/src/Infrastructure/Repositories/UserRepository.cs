using Microsoft.EntityFrameworkCore;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Infrastructure.Data
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(m => m.Role).ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.Include(m => m.Role).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> FindByUsername(string username)
        {
            return await _context
                .Users.Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
