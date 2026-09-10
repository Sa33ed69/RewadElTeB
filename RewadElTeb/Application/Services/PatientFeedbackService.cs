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
    public class PatientFeedbackService : IPatientFeedbackService
    {
        private readonly IGenericRepository<PatientFeedback> _repository;
        private readonly IMapper _mapper;
            
        public PatientFeedbackService(
            IGenericRepository<PatientFeedback> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result> CreateAsync(
      CreatePatientFeedbackDto dto,
      CancellationToken cancellationToken)
        {
            var feedback = _mapper.Map<PatientFeedback>(dto);

            await _repository.AddAsync(feedback, cancellationToken);

            return Result.Success("Your message has been sent successfully.");
        }

        public async Task<Result<List<PatientFeedbackDto>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var feedbacks = await _repository.GetAllAsync(cancellationToken);

            var result = _mapper.Map<List<PatientFeedbackDto>>(feedbacks);

            return Result<List<PatientFeedbackDto>>.Success(result);
        }

        public async Task<Result<PatientFeedbackDto>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var feedback = await _repository.GetByIdAsync(id, cancellationToken);

            if (feedback == null)
            {
                return Result<PatientFeedbackDto>.Failure(
                    "Patient feedback not found");
            }

            var result = _mapper.Map<PatientFeedbackDto>(feedback);

            return Result<PatientFeedbackDto>.Success(result);
        }
    }
}

