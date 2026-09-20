using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HomeAppointmentService : IHomeAppointmentService
    {
        private readonly IGenericRepository<HomeAppointment> _repository;
        private readonly IMapper _mapper;

        public HomeAppointmentService(
            IGenericRepository<HomeAppointment> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result> CreateAsync(
     CreateHomeAppointmentDto dto,
     CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(dto.PatientName))
            {
                return Result.Failure(
                    "Patient name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.PatientPhone))
            {
                return Result.Failure(
                    "Patient phone is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Address))
            {
                return Result.Failure(
                    "Address is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.CaseDescription))
            {
                return Result.Failure(
                    "Case description is required.");
            }

            if (!Enum.IsDefined(typeof(HomeServiceType), dto.ServiceType))
            {
                return Result.Failure(
                    "Invalid home service type.");
            }

            var homeAppointment = _mapper.Map<HomeAppointment>(dto);

            homeAppointment.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(
                homeAppointment,
                cancellationToken);

            return Result.Success(
                "Home appointment created successfully.");
        }

        public async Task<Result<IEnumerable<HomeAppointmentResponseDto>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var appointments = await _repository.GetAllAsync(
                cancellationToken);

            var response = _mapper.Map<IEnumerable<HomeAppointmentResponseDto>>(
                appointments);

            return Result<IEnumerable<HomeAppointmentResponseDto>>.Success(
                response);
        }

        public async Task<Result<HomeAppointmentResponseDto>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var homeAppointment = await _repository.GetByIdAsync(
                id,
                cancellationToken);

            if (homeAppointment == null)
            {
                return Result<HomeAppointmentResponseDto>.Failure(
                    "Home appointment not found.");
            }

            var response = _mapper.Map<HomeAppointmentResponseDto>(
                homeAppointment);

            return Result<HomeAppointmentResponseDto>.Success(response);
        }

        public async Task<Result> DeleteAsync(
      int id,
      CancellationToken cancellationToken)
        {
            var homeAppointment = await _repository.GetByIdAsync(
                id,
                cancellationToken);

            if (homeAppointment == null)
            {
                return Result.Failure(
                    "Home appointment not found.");
            }

            await _repository.DeleteAsync(
                homeAppointment,
                cancellationToken);

            return Result.Success(
                "Home appointment deleted successfully.");
        }
        public async Task<Result<IEnumerable<HomeServiceTypeDto>>> GetServiceTypesAsync(
        CancellationToken cancellationToken)
        {
            var serviceTypes = Enum.GetValues<HomeServiceType>()
                .Select(x => new HomeServiceTypeDto
                {
                    Id = (int)x,
                    Name = x.ToString()
                })
                .ToList();

            return Result<IEnumerable<HomeServiceTypeDto>>.Success(
                serviceTypes);
        }
    }
}
