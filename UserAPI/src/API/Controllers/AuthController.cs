using Microsoft.AspNetCore.Mvc;
using UserAPI.API.DTOs;
using UserAPI.Application.Helpers;
using UserAPI.Application.Interfaces;
using UserAPI.Application.Services;

namespace UserAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginDTO userLoginDTO)
        {
            var userTokenPayloadDTO = await _authService.LoginUser(userLoginDTO);
            if (userTokenPayloadDTO == null)
                return Unauthorized("Invalid Credentials");

            var token = _tokenService.GenerateToken(userTokenPayloadDTO);
            return Ok(token);
        }
    }
}
