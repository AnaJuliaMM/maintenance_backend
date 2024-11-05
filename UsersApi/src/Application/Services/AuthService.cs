using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserAuth.API.DTOs;
using UserAuth.Application.Helpers;
using UserAuth.Application.Interfaces;
using UserAuth.Domain.Entities;
using UserAuth.Domain.Interfaces;

namespace UserAuth.Application.Services
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
            var user = await _userRepository.FindUserByUsername(userLoginDTO.Username);
            if (user != null)
                if (PasswordHelper.VerifyPassword(userLoginDTO.Password, user.Password))
                    return _mapper.Map<UserTokenPayloadDTO>(user);

                    
            return null;
        }
    }
}
