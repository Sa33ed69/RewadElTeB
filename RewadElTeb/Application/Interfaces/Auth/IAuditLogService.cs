using Application.DTOs.IdentityDtos;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface IAuditLogService
    {
        Task<Result<List<AuditLogDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
