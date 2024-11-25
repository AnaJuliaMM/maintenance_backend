using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens; // Biblioteca padrao do C#
using UserAPI.API.DTOs;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Services
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
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? String.Empty)
            );
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Build header payload
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Build token payload
            double expires_in = Convert.ToDouble(_configuration["Jwt:expiration"]);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[]
                {
                    new Claim(type: ClaimTypes.Name, userTokenPayloadDTO.Name),
                    new Claim(type: ClaimTypes.Role, userTokenPayloadDTO.Role ?? "user:read"),
                },
                expires: DateTime.Now.AddHours(expires_in),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        //TODO: TESTAR
        public string RefreshToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] secret_key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);

            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret_key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = false,
                    },
                    out SecurityToken validatedToken
                );

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Check if token is valid
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    throw new SecurityTokenExpiredException("Token expirado.");
                }

                // Extract payload claims from token
                var userTokenPayloadDTO = new UserTokenPayloadDTO
                {
                    Name = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
                    Role = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value,
                };

                // Return a new token
                return GenerateToken(userTokenPayloadDTO);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Token inválido ou expirado.", ex);
            }
        }
    }
}
