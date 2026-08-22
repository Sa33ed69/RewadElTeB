using Application.DTOs.IdentityDtos;
using Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _authService
                .LoginAsync(dto, cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result.Message);

            return Ok(new
            {
                token = result.Data
            });
        }
    }
}