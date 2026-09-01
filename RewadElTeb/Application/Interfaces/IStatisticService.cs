using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStatisticService
    {
        Task<Result<IEnumerable<StatisticDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<StatisticDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<StatisticDto>> CreateAsync(CreateStatisticDto dto, CancellationToken cancellationToken); 
        Task<Result> UpdateAsync(int id, UpdateStatisticDto dto, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
