using Application.DTOs;
using Application.Interfaces;
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
            [FromForm] CreateDoctorDto dto)
        {
            var result = await _doctorService.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // PUT: api/dashboard/doctors/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateDoctorDto dto)
        {
            var result = await _doctorService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // DELETE: api/dashboard/doctors/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // GET: api/dashboard/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();

            return Ok(doctors);
        }

        // GET: api/dashboard/doctors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);

            if (doctor == null)
                return NotFound(
                    $"Doctor with ID {id} does not exist.");

            return Ok(doctor);
        }
    }
}