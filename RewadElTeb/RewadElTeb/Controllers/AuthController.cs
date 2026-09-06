using Application.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(
        CreateAdminDto dto,
        CancellationToken cancellationToken)
        {
            var result = await _authService
                .CreateAdminAsync(dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(
        CancellationToken cancellationToken)
        {
            var result = await _authService.GetRolesAsync(
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}