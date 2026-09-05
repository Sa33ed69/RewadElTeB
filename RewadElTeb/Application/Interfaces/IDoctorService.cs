using Application.DTOs;
using Application.ResultPattern;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Result> CreateAsync(
            CreateDoctorDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(
            int id,
            UpdateDoctorDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<DoctorDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<DoctorDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<DoctorStatusDto>>> GetStatusAsync();
        Task<Result> UpdateStatusAsync(int id,DoctorStatus status,CancellationToken cancellationToken = default);
    }
}