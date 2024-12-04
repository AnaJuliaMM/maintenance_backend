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

        public async Task<RoleDTO> Add(RoleDTO roleDTO)
        {
            if (roleDTO == null)
            {
                throw new ArgumentNullException(nameof(RoleDTO), "Nenhum dado foi recebido.");
            }

            Role role = _mapper.Map<Role>(roleDTO);

            role = await _roleRepository.Add(role);
            RoleDTO createdRoleDTO = _mapper.Map<RoleDTO>(role);

            return createdRoleDTO;
        }

        public async Task Update(int id, RoleDTO roleDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (roleDTO == null)
            {
                throw new ArgumentNullException(nameof(roleDTO), "Nenhum dado foi recebido.");
            }

            Role? Role = await _roleRepository.GetById(id);

            if (Role == null)
            {
                throw new KeyNotFoundException($"Papel com ID {id} não encontrado.");
            }

            _mapper.Map(roleDTO, Role);

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
