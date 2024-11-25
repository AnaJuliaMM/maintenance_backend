using Microsoft.AspNetCore.Mvc;
using UserAPI.API.DTOs;
using UserAPI.Domain.Entities;

namespace UserAPI.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(UserTokenPayloadDTO userTokenPayloadDTO);

        public string RefreshToken(string token);
    }
}
