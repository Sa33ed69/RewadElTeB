using Application.DTOs.IdentityDtos;
using Application.ResultPattern;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginDto dto,CancellationToken cancellationToken = default);

        Task<Result> CreateAdminAsync(CreateAdminDto dto,CancellationToken cancellationToken = default);
    }
}