using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceService
    {
        Task<Result> CreateAsync(
            CreateServiceDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(
            int id,
            UpdateServiceDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ServiceDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<ServiceDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
