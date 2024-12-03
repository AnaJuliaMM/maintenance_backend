using AutoMapper;
using UserAPI.API.DTOs;
using UserAPI.Application.Helpers;
using UserAPI.Application.Interfaces;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces;

namespace UserAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserTokenPayloadDTO?> LoginUser(UserLoginDTO userLoginDTO)
        {
            User? user = await _userRepository.FindByUsername(userLoginDTO.Username);

            if (user != null)
                if (PasswordHelper.VerifyPassword(userLoginDTO.Password, user.Password))
                    return _mapper.Map<UserTokenPayloadDTO>(user);

            return null;
        }
    }
}
