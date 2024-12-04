using AutoMapper;
using UserAPI.API.DTOs;
using UserAPI.Application.Interfaces;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Application.Services
{
    public class RoleService(IRoleRepository roleRepository, IMapper mapper) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<RoleDTO>> GetAll()
        {
            IEnumerable<Role> categories = await _roleRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleDTO>>(categories);
        }

        public async Task<RoleDTO?> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            Role? Role = await _roleRepository.GetById(id);

            if (Role == null)
            {
                throw new KeyNotFoundException($"Papel com ID {id} não encontrado.");
            }

            return Role != null ? _mapper.Map<RoleDTO>(Role) : null;
        }

        public async Task Add(RoleDTO RoleDTO)
        {
            if (RoleDTO == null)
            {
                throw new ArgumentNullException(nameof(RoleDTO), "Nenhum dado foi recebido.");
            }

            Role Role = _mapper.Map<Role>(RoleDTO);
            await _roleRepository.Add(Role);
        }

        public async Task Update(int id, RoleDTO RoleDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (RoleDTO == null)
            {
                throw new ArgumentNullException(nameof(RoleDTO), "Nenhum dado foi recebido.");
            }

            Role? Role = await _roleRepository.GetById(id);

            if (Role == null)
            {
                throw new KeyNotFoundException($"Papel com ID {id} não encontrado.");
            }

            _mapper.Map(RoleDTO, Role);

            await _roleRepository.Update(Role);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            Role? Role = await _roleRepository.GetById(id);

            if (Role == null)
            {
                throw new KeyNotFoundException($"Papel com ID {id} não encontrado.");
            }

            await _roleRepository.Delete(id);
        }
    }
}
