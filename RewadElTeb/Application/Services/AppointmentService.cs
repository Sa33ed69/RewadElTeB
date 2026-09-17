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
                appointment.AppointmentDate = dto.AppointmentDate.Date;
                appointment.Status = AppointmentStatus.Pending;
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
                    x => x.Doctor,
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
                    x => x.Doctor,
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

        public async Task<Result<List<AvailableAppointmentDayDto>>> GetAvailableDaysAsync(
    int doctorId,
    CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(
                doctorId,
                cancellationToken);

            if (doctor == null)
            {
                return Result<List<AvailableAppointmentDayDto>>.Failure(
                    "Doctor not found.");
            }

            if (doctor.Status != DoctorStatus.Active)
            {
                return Result<List<AvailableAppointmentDayDto>>.Failure(
                    "Doctor is not active.");
            }

            var workingDays = JsonSerializer.Deserialize<List<string>>(
                doctor.WorkingDays) ?? new List<string>();

            var today = DateTime.Today;

            // Current week starts on Monday
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;

            var weekStart = today.AddDays(-diff).Date;
            var weekEnd = weekStart.AddDays(6).Date;

            var result = new List<AvailableAppointmentDayDto>();

            for (var date = weekStart; date <= weekEnd; date = date.AddDays(1))
            {
                // Don't show past dates
                if (date < today)
                    continue;

                var dayName = date.DayOfWeek.ToString();

                if (workingDays.Contains(dayName))
                {
                    result.Add(new AvailableAppointmentDayDto
                    {
                        Date = date,
                        Day = dayName
                    });
                }
            }

            return Result<List<AvailableAppointmentDayDto>>.Success(result);
        }
    }
}