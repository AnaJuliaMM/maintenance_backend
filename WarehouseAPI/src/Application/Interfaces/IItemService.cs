using WarehouseAPI.API.DTOs;

namespace WarehouseAPI.Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAll();
        Task<ItemDTO?> GetById(int id);
        Task Add(ItemDTO itemDTO);
        Task Update(int id, ItemDTO itemDTO);
        Task Delete(int id);
    }
}
