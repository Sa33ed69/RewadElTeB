using Application.DTOs;
using Application.Interfaces;
using Application.IRepositories;
using Application.ResultPattern;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Text.Json;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IGenericRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public AppointmentService(
            IGenericRepository<Appointment> appointmentRepository,
            IGenericRepository<Doctor> doctorRepository,
            IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<Result<AppointmentDto>> CreateAsync(
            int doctorId,
            CreateAppointmentDto dto,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(
                 doctorId,
                 x => x.Department,
                 cancellationToken);

            if (doctor == null)
            {
                return Result<AppointmentDto>.Failure(
                    "Doctor not found.");
            }

            if (doctor.Status != DoctorStatus.Active)
            {
                return Result<AppointmentDto>.Failure(
                    "Doctor is not active.");
            }

            if (dto.AppointmentDate.Date < DateTime.Today)
            {
                return Result<AppointmentDto>.Failure(
                    "Appointment date cannot be in the past.");
            }

            var workingDays = JsonSerializer.Deserialize<List<string>>(
                doctor.WorkingDays) ?? new List<string>();

            var selectedDay = dto.AppointmentDate.DayOfWeek.ToString();

            if (!workingDays.Contains(selectedDay))
            {
                return Result<AppointmentDto>.Failure(
                    "Doctor is not available on this day.");
            }

            var appointment = _mapper.Map<Appointment>(dto);

                appointment.DoctorId = doctorId;
                appointment.Doctor = doctor;
                appointment.AppointmentDate = dto.AppointmentDate.Date;
                appointment.CreatedAt = DateTime.UtcNow;

            await _appointmentRepository.AddAsync(
                appointment,
                cancellationToken);

            var result = _mapper.Map<AppointmentDto>(appointment);

            return Result<AppointmentDto>.Success(result);
        }

        public async Task<Result<List<AppointmentDto>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var appointments =
             await _appointmentRepository.GetAllWithIncludesAsync(
                 x => x.Doctor.Department,
                 cancellationToken);

            var result = _mapper.Map<List<AppointmentDto>>(appointments);

            return Result<List<AppointmentDto>>.Success(result);
        }

        public async Task<Result<AppointmentDto>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var appointment =
             await _appointmentRepository.GetByIdAsync(
                 id,
                 x => x.Doctor.Department,
                 cancellationToken);

            if (appointment == null)
            {
                return Result<AppointmentDto>.Failure(
                    "Appointment not found.");
            }

            var result = _mapper.Map<AppointmentDto>(appointment);

            return Result<AppointmentDto>.Success(result);
        }

        public async Task<Result> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var appointment =
                await _appointmentRepository.GetByIdAsync(
                    id,
                    cancellationToken);

            if (appointment == null)
            {
                return Result.Failure(
                    "Appointment not found.");
            }

            await _appointmentRepository.DeleteAsync(
                appointment,
                cancellationToken);

            return Result.Success(
                "Appointment deleted successfully.");
        }

       
    }
}