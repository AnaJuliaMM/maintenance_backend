using AutoMapper;
using UserAPI.API.DTOs;
using UserAPI.Application.Helpers;
using UserAPI.Application.Interfaces;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            User? user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }

            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task Add(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "Nenhum dado foi recebido.");
            }

            User user = _mapper.Map<User>(userDTO);

            await _userRepository.Add(user);
        }

        public async Task Update(int id, UserDTO userDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "Nenhum dado foi recebido.");
            }

            User? user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }

            _mapper.Map(userDTO, user);
            await _userRepository.Update(user);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            User? user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }
            await _userRepository.Delete(id);
        }
    }
}
