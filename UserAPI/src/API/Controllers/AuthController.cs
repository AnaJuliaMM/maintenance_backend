using Microsoft.AspNetCore.Mvc;
using UserAPI.API.DTOs;
using UserAPI.Application.Interfaces;

namespace UserAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService, ITokenService tokenService)
        : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginDTO userLoginDTO)
        {
            var userTokenPayloadDTO = await _authService.LoginUser(userLoginDTO);

            if (userTokenPayloadDTO == null)
                return Unauthorized("Credencial Inválida");

            var token = _tokenService.GenerateToken(userTokenPayloadDTO);

            return Ok(token);
        }
    }
}
