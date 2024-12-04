using AutoMapper;
using UserAPI.API.DTOs;
using UserAPI.Application.Helpers;
using UserAPI.Application.Interfaces;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

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

        public async Task<UserDTO> Add(CreateUpdateUserDTO userDTO)
        {
            if (userDTO == null || userDTO.Password == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "Um ou mais campos ausentes.");
            }

            string? hashedPassword = PasswordHelper.HashPassword(userDTO.Password);
            userDTO.Password = hashedPassword;

            User user = _mapper.Map<User>(userDTO);
            user = await _userRepository.Add(user);

            UserDTO createdUserDTO = _mapper.Map<UserDTO>(user);
            return createdUserDTO;
        }

        public async Task Update(int id, CreateUpdateUserDTO userDTO)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO), "Nenhum dado foi recebido.");
            }

            User? user =
                await _userRepository.GetById(id)
                ?? throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");

            if (userDTO.Password != null)
            {
                string? hashedPassword = PasswordHelper.HashPassword(userDTO.Password);
                userDTO.Password = hashedPassword;
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
