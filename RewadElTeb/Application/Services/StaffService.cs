using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IGenericRepository<Staff> _staffRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public StaffService(
            IGenericRepository<Staff> staffRepository,
            IMapper mapper,
            IImageService imageService)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        // CREATE
        public async Task<Result> CreateAsync(
    CreateStaffDto dto,
    CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existingStaff = await _staffRepository.GetAllAsync(
                cancellationToken);

            var isDuplicate = existingStaff.Any(s =>
                s.Name == dto.Name &&
                s.Role == dto.Role &&
                s.Description == dto.Description);

            if (isDuplicate)
            {
                return Result.Failure(
                    "A staff member with the same data already exists.");
            }

            try
            {
                var staff = _mapper.Map<Staff>(dto);

                if (dto.Image != null)
                {
                    staff.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "Staff");
                }

                await _staffRepository.AddAsync(
                    staff,
                    cancellationToken);

                return Result.Success(
                    "Staff member created successfully.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Result.Failure(
                    $"Failed to create staff member: {ex.Message}");
            }
        }

        // UPDATE
        public async Task<Result> UpdateAsync(
            int id,
            UpdateStaffDto dto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var staff =
                await _staffRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (staff == null)
            {
                return Result.Failure(
                    $"Staff member with ID {id} does not exist.");
            }

            try
            {
                var oldImageUrl = staff.ImageUrl;

                _mapper.Map(dto, staff);

                // Save new image
                if (dto.Image != null)
                {
                    staff.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "Staff");

                    // Delete old image
                    if (!string.IsNullOrEmpty(oldImageUrl))
                    {
                        await _imageService.DeleteImageAsync(
                            oldImageUrl,
                            "Staff");
                    }
                }

                await _staffRepository.UpdateAsync(
                    staff,
                    cancellationToken);

                return Result.Success(
                    "Staff member updated successfully.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to update staff member.");
            }
        }

        // DELETE
        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var staff =
                await _staffRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (staff == null)
            {
                return Result.Failure(
                    $"Staff member with ID {id} does not exist.");
            }

            try
            {
                // Delete image from wwwroot/images/Staff
                if (!string.IsNullOrEmpty(staff.ImageUrl))
                {
                    await _imageService.DeleteImageAsync(
                        staff.ImageUrl,
                        "Staff");
                }

                // Delete staff from database
                await _staffRepository.DeleteAsync(
                    staff,
                    cancellationToken);

                return Result.Success(
                    "Staff member deleted successfully.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to delete staff member.");
            }
        }

        // GET ALL
        public async Task<IEnumerable<StaffDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var staff =
                await _staffRepository.GetAllAsync(
                    cancellationToken);

            return _mapper.Map<IEnumerable<StaffDto>>(staff);
        }

        // GET BY ID
        public async Task<StaffDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var staff =
                await _staffRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (staff == null)
                return null;

            return _mapper.Map<StaffDto>(staff);
        }
    }
}