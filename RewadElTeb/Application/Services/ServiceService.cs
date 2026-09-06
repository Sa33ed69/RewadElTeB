using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IGenericRepository<Service> _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ServiceService(
            IGenericRepository<Service> serviceRepository,
            IMapper mapper,
            IImageService imageService)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _imageService = imageService;
        }
        public async Task<Result> CreateAsync(CreateServiceDto dto,CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existingServices = await _serviceRepository.GetAllAsync(cancellationToken);

            var isDuplicate = existingServices.Any(s =>
                s.Name == dto.Name &&
                s.Description == dto.Description);

            if (isDuplicate)
            {
                return Result.Failure(
                    "A service with the same data already exists.");
            }

            try
            {
                var service = _mapper.Map<Service>(dto);

                if (dto.Image != null)
                {
                    service.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "Services");
                }

                await _serviceRepository.AddAsync(
                    service,
                    cancellationToken);

                return Result.Success(
                    "Service created successfully.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Result.Failure(
                    $"Failed to create service: {ex.Message}");
            }
        }

        // UPDATE
        public async Task<Result> UpdateAsync(
            int id,
            UpdateServiceDto dto,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var service =
                await _serviceRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (service == null)
            {
                return Result.Failure(
                    $"Service with ID {id} does not exist.");
            }

            try
            {
                var oldImageUrl = service.ImageUrl;

                _mapper.Map(dto, service);

                // Save new image
                if (dto.Image != null)
                {
                    service.ImageUrl =
                        await _imageService.SaveImageAsync(
                            dto.Image,
                            "Services");

                    // Delete old image
                    if (!string.IsNullOrEmpty(oldImageUrl))
                    {
                        await _imageService.DeleteImageAsync(
                            oldImageUrl,
                            "Services");
                    }
                }

                await _serviceRepository.UpdateAsync(
                    service,
                    cancellationToken);

                return Result.Success(
                    "Service updated successfully.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to update service.");
            }
        }

        // DELETE
        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var service =
                await _serviceRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (service == null)
            {
                return Result.Failure(
                    $"Service with ID {id} does not exist.");
            }

            try
            {
                // Delete image
                if (!string.IsNullOrEmpty(service.ImageUrl))
                {
                    await _imageService.DeleteImageAsync(
                        service.ImageUrl,
                        "Services");
                }

                // Delete service from database
                await _serviceRepository.DeleteAsync(
                    service,
                    cancellationToken);

                return Result.Success(
                    "Service deleted successfully.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to delete service.");
            }
        }

        // GET ALL
        public async Task<IEnumerable<ServiceDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var services =
                await _serviceRepository.GetAllAsync(
                    cancellationToken);

            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        // GET BY ID
        public async Task<ServiceDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var service =
                await _serviceRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (service == null)
                return null;

            return _mapper.Map<ServiceDto>(service);
        }
    }
}

