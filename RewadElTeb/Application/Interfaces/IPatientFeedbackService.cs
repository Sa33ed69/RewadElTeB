using Application.DTOs;
using Application.ResultPattern;


namespace Application.Interfaces
{           
    public interface IPatientFeedbackService
    {
        Task<Result> CreateAsync(
            CreatePatientFeedbackDto dto,
            CancellationToken cancellationToken);
        Task<Result<List<PatientFeedbackDto>>> GetAllAsync(CancellationToken cancellationToken);

        Task<Result<PatientFeedbackDto>> GetByIdAsync(int id,CancellationToken cancellationToken);

    }
}
