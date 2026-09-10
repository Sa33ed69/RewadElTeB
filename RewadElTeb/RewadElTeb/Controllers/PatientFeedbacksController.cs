using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientFeedbacksController : ControllerBase
    {
        private readonly IPatientFeedbackService _patientFeedbackService;

        public PatientFeedbacksController(
            IPatientFeedbackService patientFeedbackService)
        {
            _patientFeedbackService = patientFeedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePatientFeedbackDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _patientFeedbackService.CreateAsync(
                dto,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await _patientFeedbackService.GetAllAsync(
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _patientFeedbackService.GetByIdAsync(
                id,
                cancellationToken);

            return Ok(result);
        }
    }
}