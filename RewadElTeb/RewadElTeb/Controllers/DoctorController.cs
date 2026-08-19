using Application.DTOs;
using Application.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorDto dto)
        {
            var result = await _doctorService.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateDoctorDto dto)
        {
            var result = await _doctorService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
