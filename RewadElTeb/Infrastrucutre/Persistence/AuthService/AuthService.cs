using Application.DTOs.IdentityDtos;
using Application.Interfaces.Auth;
using Application.ResultPattern;
using Infrastructure.Persistence.Identity;
using Infrastructure.Persistence.JwtModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<Result<string>> LoginAsync(
           LoginDto dto)
        {
            var user = await _userManager
                .FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Result<string>.Failure(
                    "Invalid email or password.");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(
                    user,
                    dto.Password,
                    false);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(
                    "Invalid email or password.");
            }

            var roles = await _userManager
                .GetRolesAsync(user);

            var token = await _jwtService.GenerateTokenAsync(
                user.Id,
                user.Email!,
                roles);

            return Result<string>.Success(token);
        }
    }
}
