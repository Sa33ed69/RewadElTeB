using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IGenericRepository<Contract> _contractRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ContractService(
            IGenericRepository<Contract> contractRepository,
            IImageService imageService,
            IMapper mapper)
        {
            _contractRepository = contractRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var contracts = await _contractRepository
                .GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ContractDto>>(contracts);
        }

        public async Task<ContractDto?> GetByIdAsync(int id,CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.GetByIdAsync(id, cancellationToken);

            if (contract == null)
                return null;

            return _mapper.Map<ContractDto>(contract);
        }

        public async Task<ContractDto> CreateAsync(CreateContractDto dto,CancellationToken cancellationToken)
        {
            var contract = _mapper.Map<Contract>(dto);

            if (dto.Image != null)
            {
                contract.ImageUrl = await _imageService.SaveImageAsync(dto.Image,"Contracts");
            }

            await _contractRepository.AddAsync(
                contract,
                cancellationToken);

            return _mapper.Map<ContractDto>(contract);
        }

        public async Task<bool> UpdateAsync(int id,UpdateContractDto dto,CancellationToken cancellationToken)
        {
            var contract = await _contractRepository
                .GetByIdAsync(id, cancellationToken);

            if (contract == null)
                return false;

            _mapper.Map(dto, contract);

            if (dto.Image != null)
            {
                contract.ImageUrl = await _imageService.SaveImageAsync(dto.Image,"Contracts");
            }

            await _contractRepository.UpdateAsync(
                contract,
                cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var contract = await _contractRepository
                .GetByIdAsync(id, cancellationToken);

            if (contract == null)
                return false;

            await _contractRepository.DeleteAsync(
                contract,
                cancellationToken);

            return true;
        }
    }
}
