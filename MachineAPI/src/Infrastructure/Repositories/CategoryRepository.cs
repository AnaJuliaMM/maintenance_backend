using Microsoft.EntityFrameworkCore;
using MachineAPI.Domain.Entities;
using MachineAPI.Domain.Interfaces;

namespace MachineAPI.Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category?> GetByName(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Label == categoryName);
        }

        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Category? category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
