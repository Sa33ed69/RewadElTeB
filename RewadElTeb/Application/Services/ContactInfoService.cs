using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly IGenericRepository<ContactInfo> _contactInfoRepository;
        private readonly IMapper _mapper;

        public ContactInfoService(
            IGenericRepository<ContactInfo> contactInfoRepository,
            IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
        }

        public async Task<Result<ContactInfoDto?>> GetAsync(
            CancellationToken cancellationToken = default)
        {
            var contactInfo = await _contactInfoRepository
                .GetAllAsync(cancellationToken);

            var contact = contactInfo.FirstOrDefault();

            if (contact == null)
            {
                return Result<ContactInfoDto?>.Success(null);
            }

            var dto = _mapper.Map<ContactInfoDto>(contact);

            return Result<ContactInfoDto?>.Success(dto);
        }

        public async Task<Result> CreateAsync(
            CreateContactInfoDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var contactInfo = _mapper.Map<ContactInfo>(dto);

                contactInfo.UpdatedAt = DateTime.UtcNow;

                await _contactInfoRepository.AddAsync(
                    contactInfo,
                    cancellationToken);

                return Result.Success(
                    "Contact information created successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to create contact information.");
            }
        }

        public async Task<Result> UpdateAsync(
            int id,
            UpdateContactInfoDto dto,
            CancellationToken cancellationToken = default)
        {
            var contactInfo = await _contactInfoRepository
                .GetByIdAsync(id, cancellationToken);

            if (contactInfo == null)
            {
                return Result.Failure(
                    $"Contact information with ID {id} does not exist.");
            }

            try
            {
                _mapper.Map(dto, contactInfo);

                contactInfo.UpdatedAt = DateTime.UtcNow;

                await _contactInfoRepository.UpdateAsync(
                    contactInfo,
                    cancellationToken);

                return Result.Success(
                    "Contact information updated successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to update contact information.");
            }
        }

        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var contactInfo = await _contactInfoRepository
                .GetByIdAsync(id, cancellationToken);

            if (contactInfo == null)
            {
                return Result.Failure(
                    $"Contact information with ID {id} does not exist.");
            }

            try
            {
                await _contactInfoRepository.DeleteAsync(
                    contactInfo,
                    cancellationToken);

                return Result.Success(
                    "Contact information deleted successfully.");
            }
            catch (Exception)
            {
                return Result.Failure(
                    "Failed to delete contact information.");
            }
        }
    }
}