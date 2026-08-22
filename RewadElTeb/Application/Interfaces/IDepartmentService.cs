using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<Result> CreateAsync(
            CreateDepartmentDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(
            int id,
            UpdateDepartmentDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<DepartmentDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<DepartmentDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<DepartmentWithDoctorsDto>> GetAllWithDoctorsAsync(
            CancellationToken cancellationToken = default);
    }
}