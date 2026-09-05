using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _doctorRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public DoctorService(
            IGenericRepository<Doctor> doctorRepository,
            IGenericRepository<Department> departmentRepository,
            IMapper mapper,
            IImageService imageService)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _imageService = imageService;
        }

        public async Task<Result> CreateAsync(
            CreateDoctorDto dto,
            CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository
                .GetByIdAsync(
                    dto.DepartmentId,
                    cancellationToken);

            if (department == null)
            {
                return Result.Failure(
                    $"Department with ID {dto.DepartmentId} does not exist.");
            }

            try
            {
                var doctor = _mapper.Map<Doctor>(dto);

                if (dto.Image != null)
                {
                    doctor.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "doctors");
                }

                await _doctorRepository.AddAsync(
                    doctor,
                    cancellationToken);

                return Result.Success("Doctor created successfully.");
            }
            catch (Exception)
            {
                return Result.Failure("Failed to create doctor.");
            }
        }

        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorRepository
                .GetByIdAsync(
                    id,
                    cancellationToken);

            if (doctor == null)
            {
                return Result.Failure(
                    $"Doctor with ID {id} does not exist.");
            }

            try
            {
                // Delete image from wwwroot
                await _imageService.DeleteImageAsync(
                    doctor.ImageUrl,
                    "doctors");

                // Delete doctor from database
                await _doctorRepository.DeleteAsync(
                    doctor,
                    cancellationToken);

                return Result.Success(
                    "Doctor deleted successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to delete doctor.");
            }
        }

        public async Task<IEnumerable<DoctorDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var doctors =
                await _doctorRepository
                    .GetAllWithIncludesAsync(
                        d => d.Department,
                        cancellationToken);

            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<DoctorDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var doctor =
                await _doctorRepository
                    .GetByIdAsync(
                        id,
                        d => d.Department,
                        cancellationToken);

            if (doctor == null)
                return null;

            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<Result<IEnumerable<DoctorStatusDto>>> GetStatusAsync()
        {
            var statuses = Enum.GetValues<DoctorStatus>()
          .Select(status => new DoctorStatusDto
          {
              Id = (int)status,
              Name = status.ToString()
          });

            return Result<IEnumerable<DoctorStatusDto>>.Success(statuses);
        }

        public async Task<Result> UpdateAsync(
            int id,
            UpdateDoctorDto dto,
            CancellationToken cancellationToken = default)
        {
            var doctor =
                await _doctorRepository
                    .GetByIdAsync(
                        id,
                        cancellationToken);

            if (doctor == null)
            {
                return Result.Failure(
                    $"Doctor with ID {id} does not exist.");
            }

            var department =
                await _departmentRepository
                    .GetByIdAsync(
                        dto.DepartmentId,
                        cancellationToken);

            if (department == null)
            {
                return Result.Failure(
                    $"Department with ID {dto.DepartmentId} does not exist.");
            }

            if (!Enum.IsDefined(
                    typeof(DoctorStatus),
                    dto.Status))
            {
                return Result.Failure(
                    $"Invalid doctor status: {(int)dto.Status}.");
            }

            try
            {
                if (dto.Image != null)
                {
                    await _imageService.DeleteImageAsync(
                        doctor.ImageUrl,
                        "doctors");

                    doctor.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "doctors");
                }

                _mapper.Map(dto, doctor);

                await _doctorRepository.UpdateAsync(
                    doctor,
                    cancellationToken);

                return Result.Success(
                    "Doctor updated successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to update doctor.");
            }
        }
        public async Task<Result> UpdateStatusAsync(
          int id,
          DoctorStatus status,
          CancellationToken cancellationToken = default)
        {
            var doctor = await _doctorRepository.GetByIdAsync(
                id,
                cancellationToken);

            if (doctor == null)
            {
                return Result.Failure(
                    $"Doctor with ID {id} does not exist.");
            }

            if (!Enum.IsDefined(typeof(DoctorStatus), status))
            {
                return Result.Failure(
                    $"Invalid doctor status: {(int)status}.");
            }

            try
            {
                doctor.Status = status;

                await _doctorRepository.UpdateAsync(
                    doctor,
                    cancellationToken);

                return Result.Success(
                    "Doctor status updated successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to update doctor status.");
            }
        }

    }
}