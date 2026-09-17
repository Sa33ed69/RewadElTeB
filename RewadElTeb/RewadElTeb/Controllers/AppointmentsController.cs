using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(
            IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("doctor/{doctorId}")]
        public async Task<IActionResult> Create(
             int doctorId,
             CreateAppointmentDto dto,
             CancellationToken cancellationToken)
        {
            var result = await _appointmentService.CreateAsync(
                doctorId,
                dto,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
           CancellationToken cancellationToken)
        {
            var result = await _appointmentService.GetAllAsync(
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/dashboard/appointments/{id}
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _appointmentService.GetByIdAsync(
                id,
                cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
       
        // DELETE: api/dashboard/appointments/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _appointmentService.DeleteAsync(
                id,
                cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
