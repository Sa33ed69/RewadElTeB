using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [ApiController]
    [Route("api/dashboard/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // POST: api/dashboard/doctors
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateDoctorDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _doctorService.CreateAsync(
                dto,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // PUT: api/dashboard/doctors/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] UpdateDoctorDto dto, CancellationToken cancellationToken)
        {
            var result = await _doctorService.UpdateAsync(
                id,
                dto,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // DELETE: api/dashboard/doctors/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _doctorService.DeleteAsync(
                id,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // GET: api/dashboard/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var doctors = await _doctorService.GetAllAsync(
                cancellationToken);

            return Ok(doctors);
        }

        // GET: api/dashboard/doctors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.GetByIdAsync(
                id,
                cancellationToken);

            if (doctor == null)
                return NotFound(
                    $"Doctor with ID {id} does not exist.");

            return Ok(doctor);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var result = await _doctorService.GetStatusAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            DoctorStatus status,
            CancellationToken cancellationToken)
        {
            var result = await _doctorService.UpdateStatusAsync(
                id,
                status,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}