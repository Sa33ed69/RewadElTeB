using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContactInfoService
    {
        Task<Result<ContactInfoDto?>> GetAsync(
            CancellationToken cancellationToken = default);

        Task<Result> CreateAsync(
            CreateContactInfoDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(
            int id,
            UpdateContactInfoDto dto,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
