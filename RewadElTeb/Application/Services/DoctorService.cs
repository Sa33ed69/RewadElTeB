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
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _doctorRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;
        public DoctorService(
            IGenericRepository<Doctor> doctorRepository,
            IGenericRepository<Department> departmentRepository,
            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        public async Task<Result> CreateAsync(CreateDoctorDto dto)
        {
            var department = await _departmentRepository
                .GetByIdAsync(dto.DepartmentId);
            if (department == null)
            {
                return Result.Failure(
                    $"Department with ID {dto.DepartmentId} does not exist.");
            }
            try
            {
                var doctor = _mapper.Map<Doctor>(dto);

                await _doctorRepository.AddAsync(doctor);

                return Result.Success("Doctor created successfully.");
            }
            catch (Exception)
            {
                return Result.Failure("Failed to create doctor.");
            }
        }

        public async Task<Result> UpdateAsync(int id,UpdateDoctorDto dto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                return Result.Failure(
                    $"Doctor with ID {id} does not exist.");
            }

            var department = await _departmentRepository
                .GetByIdAsync(dto.DepartmentId);

            if (department == null)
            {
                return Result.Failure(
                    $"Department with ID {dto.DepartmentId} does not exist.");
            }
            if (!Enum.IsDefined(typeof(DoctorStatus), dto.Status))
            {
                return Result.Failure(
                    $"Invalid doctor status: {(int)dto.Status}.");
            }
            try
            {
                _mapper.Map(dto, doctor);

                await _doctorRepository.UpdateAsync(doctor);

                return Result.Success(
                    "Doctor updated successfully.");
            }
            catch (Exception)
            {
                return Result.Failure("Failed to update doctor.");
            }
        }
    }
}
