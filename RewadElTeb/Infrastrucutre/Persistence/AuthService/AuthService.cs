using Application.DTOs.IdentityDtos;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _roleManager = roleManager;

        }

        public async Task<Result<string>> LoginAsync(
            LoginDto dto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager
                .FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Result<string>.Failure(
                    "Invalid email or password.");
            }

            cancellationToken.ThrowIfCancellationRequested();

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

            cancellationToken.ThrowIfCancellationRequested();

            var roles = await _userManager
                .GetRolesAsync(user);

            var token = await _jwtService.GenerateTokenAsync(
                user.Id,
                user.Email!,
                roles);

            return Result<string>.Success(token);
        }

        public async Task<Result> CreateAdminAsync(
    CreateAdminDto dto,
    CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existingUser = await _userManager
                .FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return Result.Failure(
                    "An account with this email already exists.");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return Result.Failure(
                    "Password and Confirm Password do not match.");
            }

            // Allow only Admin or Manager
            if (dto.Role != "Admin" && dto.Role != "Manager")
            {
                return Result.Failure(
                    "Invalid role.");
            }

            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email
                };

                var createResult = await _userManager
                    .CreateAsync(user, dto.Password);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        createResult.Errors.Select(e => e.Description));

                    return Result.Failure(errors);
                }

                cancellationToken.ThrowIfCancellationRequested();

                var roleResult = await _userManager
                    .AddToRoleAsync(user, dto.Role);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        roleResult.Errors.Select(e => e.Description));

                    await _userManager.DeleteAsync(user);

                    return Result.Failure(errors);
                }

                return Result.Success(
                    $"{dto.Role} account created successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to create account.");
            }
        }
        public async Task<Result<List<RoleDto>>> GetRolesAsync(
       CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var roles = _roleManager.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name!
                })
                .ToList();

            return Result<List<RoleDto>>.Success(roles);
        }

    }
}