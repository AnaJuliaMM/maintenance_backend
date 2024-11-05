using Microsoft.AspNetCore.Mvc;
using UserAuth.API.DTOs;
using UserAuth.Domain.Entities;

namespace UserAuth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserTokenPayloadDTO?> LoginUser(UserLoginDTO userLoginDTO);
    }
}
