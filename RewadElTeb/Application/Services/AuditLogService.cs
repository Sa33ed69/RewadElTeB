using Application.DTOs.IdentityDtos;
using Application.Interfaces.Auth;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IGenericRepository<AuditLog> _auditLogRepository;
        private readonly IMapper _mapper;

        public AuditLogService(
            IGenericRepository<AuditLog> auditLogRepository,
            IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<AuditLogDto>>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var logs = await _auditLogRepository.GetAllAsync();

            var result = _mapper.Map<List<AuditLogDto>>(logs);

            return Result<List<AuditLogDto>>.Success(result);
        }
    }
}
