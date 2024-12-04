using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item?> GetById(int id);
        Task<Item> Add(Item item);
        Task Update(Item item);
        Task Delete(int id);
    }
}
