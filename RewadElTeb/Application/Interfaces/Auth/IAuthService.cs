using Application.DTOs;
using Application.DTOs.IdentityDtos;
using Application.ResultPattern;

public interface IAuthService
{
    Task<Result<string>> LoginAsync(LoginDto dto,CancellationToken cancellationToken = default);
    Task<Result> CreateAdminAsync(CreateAdminDto dto,CancellationToken cancellationToken = default);
    Task<Result<List<RoleDto>>> GetRolesAsync(CancellationToken cancellationToken = default);
    Task<Result> DeleteManagerAsync( string userId,CancellationToken cancellationToken = default);
    Task<Result<List<UserDto>>> GetUsersAsync(CancellationToken cancellationToken = default);
}