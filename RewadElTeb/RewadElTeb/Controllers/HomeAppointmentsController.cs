using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAppointmentsController : ControllerBase
    {
        private readonly IHomeAppointmentService _homeAppointmentService;

        public HomeAppointmentsController(
            IHomeAppointmentService homeAppointmentService)
        {
            _homeAppointmentService = homeAppointmentService;
        }
        // GET: api/HomeAppointments/service-types
        [HttpGet("service-types")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceTypes(
            CancellationToken cancellationToken)
        {
            var result = await _homeAppointmentService.GetServiceTypesAsync(
                cancellationToken);

            return Ok(result);
        }

        // POST: api/HomeAppointments
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateHomeAppointmentDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _homeAppointmentService.CreateAsync(
                dto,
                cancellationToken);

            return Ok(result);
        }

        // GET: api/HomeAppointments
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await _homeAppointmentService.GetAllAsync(
                cancellationToken);

            return Ok(result);
        }

        // GET: api/HomeAppointments/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _homeAppointmentService.GetByIdAsync(
                id,
                cancellationToken);

            return Ok(result);
        }

        // DELETE: api/HomeAppointments/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _homeAppointmentService.DeleteAsync(
                id,
                cancellationToken);

            return Ok(result);
        }
    }
}
