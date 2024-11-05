using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens; // Biblioteca padrao do C#
using UserAuth.API.DTOs;
using UserAuth.Application.Interfaces;
using UserAuth.Domain.Entities;

namespace UserAuth.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserTokenPayloadDTO userTokenPayloadDTO)
        {
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"] ?? String.Empty
                )
            );
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Header
            var credentials = new SigningCredentials(
                secretKey,
                SecurityAlgorithms.HmacSha256 // Seguro, mas nem tanto "comum"
            );

   


            // Payload
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new []
                {
                    new Claim(type: ClaimTypes.Name, userTokenPayloadDTO.Name),
                    new Claim(type: ClaimTypes.Role, userTokenPayloadDTO.Role ?? "user:read")
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public string RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
