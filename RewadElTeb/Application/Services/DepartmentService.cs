using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public DepartmentService(
            IGenericRepository<Department> departmentRepository,
            IMapper mapper,
            IImageService imageService)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        // CREATE
        public async Task<Result> CreateAsync(
            CreateDepartmentDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var department = _mapper.Map<Department>(dto);

                // Save Image
                if (dto.Image != null)
                {
                    department.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "Departments");
                }

                await _departmentRepository.AddAsync(
                    department,
                    cancellationToken);

                return Result.Success(
                    "Department created successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to create department.");
            }
        }

        // UPDATE
        public async Task<Result> UpdateAsync(
            int id,
            UpdateDepartmentDto dto,
            CancellationToken cancellationToken = default)
        {
            var department =
                await _departmentRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (department == null)
            {
                return Result.Failure(
                    $"Department with ID {id} does not exist.");
            }

            try
            {
                var oldImageUrl = department.ImageUrl;

                // Update Department Data
                _mapper.Map(dto, department);

                // Update Image only if a new image was sent
                if (dto.Image != null)
                {
                    department.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "Departments");

                    // Delete old image
                    if (!string.IsNullOrEmpty(oldImageUrl))
                    {
                        await _imageService.DeleteImageAsync(
                            oldImageUrl,
                            "Departments");
                    }
                }

                await _departmentRepository.UpdateAsync(
                    department,
                    cancellationToken);

                return Result.Success(
                    "Department updated successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to update department.");
            }
        }

        // DELETE
        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var department =
                await _departmentRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (department == null)
            {
                return Result.Failure(
                    $"Department with ID {id} does not exist.");
            }

            try
            {
                // Delete Image
                if (!string.IsNullOrEmpty(department.ImageUrl))
                {
                    await _imageService.DeleteImageAsync(
                        department.ImageUrl,
                        "Departments");
                }

                // Delete Department
                await _departmentRepository.DeleteAsync(
                    department,
                    cancellationToken);

                return Result.Success(
                    "Department deleted successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to delete department.");
            }
        }

        // GET ALL
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var departments =
                await _departmentRepository.GetAllAsync(
                    cancellationToken);

            return _mapper.Map<IEnumerable<DepartmentDto>>(
                departments);
        }

        // GET BY ID
        public async Task<DepartmentDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var department =
                await _departmentRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (department == null)
                return null;

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<IEnumerable<DepartmentWithDoctorsDto>>
            GetAllWithDoctorsAsync(
                CancellationToken cancellationToken = default)
        {
            var departments =
                await _departmentRepository
                    .GetAllWithIncludesAsync(
                        d => d.Doctors,
                        cancellationToken);

            return _mapper.Map<IEnumerable<DepartmentWithDoctorsDto>>(
                departments);
        }
    }
}