using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces;

namespace WarehouseAPI.Infrastructure.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> GetById(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item> Add(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task Update(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Item? item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
