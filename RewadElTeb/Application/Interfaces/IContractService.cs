using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<ContractDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken);

        Task<ContractDto> CreateAsync(
            CreateContractDto dto,
            CancellationToken cancellationToken);

        Task<bool> UpdateAsync(
            int id,
            UpdateContractDto dto,
            CancellationToken cancellationToken);

        Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken);
    }
}
