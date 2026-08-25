using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStaffService 
    {
        Task<Result> CreateAsync(CreateStaffDto dto,CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(int id,UpdateStaffDto dto,CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(int id,CancellationToken cancellationToken = default);

        Task<IEnumerable<StaffDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<StaffDto?> GetByIdAsync(int id,CancellationToken cancellationToken = default);
    }
}
