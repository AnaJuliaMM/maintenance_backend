using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserAuth.API.DTOs;
using UserAuth.Application.Helpers;
using UserAuth.Application.Interfaces;
using UserAuth.Application.Services;

namespace UserAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshTokenController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {   
            // Extract token from request header
            string authHeader = HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedAccessException("Token não encontrado no cabeçalho.");
            }

            string token = authHeader["Bearer ".Length..].Trim();

             try
            {
                // Chama o método RefreshToken do serviço de autenticação
                var newToken = await Task.Run(() => _tokenService.RefreshToken(token));

                // Retorna o novo token para o cliente
                return Ok(new { token = newToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a requisição.");
            }

            

        }
    }
}
