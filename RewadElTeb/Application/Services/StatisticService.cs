using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IGenericRepository<Statistic> _statisticRepository;
        private readonly IMapper _mapper;


    public StatisticService(
        IGenericRepository<Statistic> statisticRepository,
        IMapper mapper)
        {
            _statisticRepository = statisticRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StatisticDto>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var statistics = await _statisticRepository
                .GetAllAsync(cancellationToken);

            var statisticDtos =
                _mapper.Map<IEnumerable<StatisticDto>>(statistics);

            return Result<IEnumerable<StatisticDto>>.Success(
                statisticDtos);
        }

        public async Task<Result<StatisticDto>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var statistic = await _statisticRepository
                .GetByIdAsync(id, cancellationToken);

            if (statistic == null)
            {
                return Result<StatisticDto>.Failure(
                    "Statistic not found");
            }

            var statisticDto =
                _mapper.Map<StatisticDto>(statistic);

            return Result<StatisticDto>.Success(
                statisticDto);
        }

        public async Task<Result<StatisticDto>> CreateAsync(
            CreateStatisticDto dto,
            CancellationToken cancellationToken)
        {
            var exists = await _statisticRepository.AnyAsync(
                x => x.Key.ToLower() == dto.Key.ToLower(),
                cancellationToken);

            if (exists)
            {
                return Result<StatisticDto>.Failure(
                    "This statistic key already exists");
            }

            var statistic = _mapper.Map<Statistic>(dto);

            statistic.UpdatedAt = DateTime.UtcNow;

            await _statisticRepository.AddAsync(
                statistic,
                cancellationToken);

            var statisticDto =
                _mapper.Map<StatisticDto>(statistic);

            return Result<StatisticDto>.Success(
                statisticDto);
        }

        public async Task<Result> UpdateAsync(
            int id,
            UpdateStatisticDto dto,
            CancellationToken cancellationToken)
        {
            var statistic = await _statisticRepository
                .GetByIdAsync(id, cancellationToken);

            if (statistic == null)
            {
                return Result.Failure(
                    "Statistic not found");
            }

            if (!string.IsNullOrWhiteSpace(dto.Key))
            {
                var exists = await _statisticRepository.AnyAsync(
                    x => x.Key.ToLower() == dto.Key.ToLower()
                         && x.Id != id,
                    cancellationToken);

                if (exists)
                {
                    return Result.Failure(
                        "This statistic key already exists");
                }
            }

            _mapper.Map(dto, statistic);

            statistic.UpdatedAt = DateTime.UtcNow;

            await _statisticRepository.UpdateAsync(
                statistic,
                cancellationToken);

            return Result.Success(
                "Statistic updated successfully");
        }

        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var statistic = await _statisticRepository
                .GetByIdAsync(id, cancellationToken);

            if (statistic == null)
            {
                return Result.Failure(
                    "Statistic not found");
            }

            await _statisticRepository.DeleteAsync(
                statistic,
                cancellationToken);

            return Result.Success(
                "Statistic deleted successfully");
        }
    }
}
