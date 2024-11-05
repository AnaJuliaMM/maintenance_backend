using Microsoft.AspNetCore.Mvc;
using UserAuth.API.DTOs;
using UserAuth.Domain.Entities;

namespace UserAuth.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(UserTokenPayloadDTO userTokenPayloadDTO);

        public string RefreshToken(string token);
    }
}
