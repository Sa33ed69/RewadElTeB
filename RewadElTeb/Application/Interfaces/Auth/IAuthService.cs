using Application.DTOs.IdentityDtos;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginDto dto,CancellationToken cancellationToken = default);
    }
}
