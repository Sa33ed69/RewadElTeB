using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHomeAppointmentService
    {
        Task<Result> CreateAsync(
         CreateHomeAppointmentDto dto,
         CancellationToken cancellationToken);

        Task<Result<IEnumerable<HomeAppointmentResponseDto>>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<Result<HomeAppointmentResponseDto>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken);
        Task<Result<IEnumerable<HomeServiceTypeDto>>> GetServiceTypesAsync(
                CancellationToken cancellationToken);
        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken);
    }
}
