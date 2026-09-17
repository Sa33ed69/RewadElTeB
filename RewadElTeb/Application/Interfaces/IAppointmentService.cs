using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentDto>> CreateAsync(
            int doctorId,
            CreateAppointmentDto dto,
            CancellationToken cancellationToken);
 

        Task<Result<List<AppointmentDto>>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<Result<AppointmentDto>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken);
    }
}
