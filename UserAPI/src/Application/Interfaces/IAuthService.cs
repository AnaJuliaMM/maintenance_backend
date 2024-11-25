using Microsoft.AspNetCore.Mvc;
using UserAPI.API.DTOs;
using UserAPI.Domain.Entities;

namespace UserAPI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserTokenPayloadDTO?> LoginUser(UserLoginDTO userLoginDTO);
    }
}
