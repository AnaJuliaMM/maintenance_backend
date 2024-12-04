using AutoMapper;
using WarehouseAPI.API.DTOs;
using WarehouseAPI.Application.Interfaces;
using WarehouseAPI.Domain.Entities;
using WarehouseAPI.Domain.Interfaces;

namespace WarehouseAPI.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDTO>> GetAll()
        {
            IEnumerable<Item> items = await _itemRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemDTO>>(items);
        }

        public async Task<ItemDTO?> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            Item? item = await _itemRepository.GetById(id);

            if (item == null)
            {
                throw new KeyNotFoundException($"Item com ID {id} não encontrada.");
            }
            return item != null ? _mapper.Map<ItemDTO>(item) : null;
        }

        public async Task<ItemDTO> Add(ItemDTO itemDTO)
        {
            if (itemDTO == null)
            {
                throw new ArgumentNullException(nameof(itemDTO), "Nenhum dado foi recebido.");
            }

            if (itemDTO.acquisitionDate.HasValue)
            {
                itemDTO.acquisitionDate = DateTime.SpecifyKind(
                    itemDTO.acquisitionDate.Value,
                    DateTimeKind.Utc
                );
            }

            Item item = _mapper.Map<Item>(itemDTO);
            item = await _itemRepository.Add(item);

            return _mapper.Map<ItemDTO>(item);
        }

        public async Task Update(int id, ItemDTO itemDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (itemDTO == null)
            {
                throw new ArgumentNullException(nameof(itemDTO), "Nenhum dado foi recebido.");
            }

            Item? item = await _itemRepository.GetById(id);

            if (item == null)
            {
                throw new KeyNotFoundException($"Item com ID {id} não encontrado.");
            }

            if (itemDTO.acquisitionDate.HasValue)
            {
                itemDTO.acquisitionDate = DateTime.SpecifyKind(
                    itemDTO.acquisitionDate.Value,
                    DateTimeKind.Utc
                );
            }
            _mapper.Map(itemDTO, item);

            await _itemRepository.Update(item);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            Item? item = await _itemRepository.GetById(id);

            if (item == null)
            {
                throw new KeyNotFoundException($"Item com ID {id} não encontrado.");
            }

            await _itemRepository.Delete(id);
        }
    }
}
