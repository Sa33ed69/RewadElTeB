using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
          [FromForm] CreateStaffDto dto,
          CancellationToken cancellationToken)
        {
            var result =
                await _staffService.CreateAsync(
                    dto,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // PUT: api/dashboard/staff/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateStaffDto dto,
            CancellationToken cancellationToken)
        {
            var result =
                await _staffService.UpdateAsync(
                    id,
                    dto,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // DELETE: api/dashboard/staff/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result =
                await _staffService.DeleteAsync(
                    id,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // GET: api/dashboard/staff
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var staff =
                await _staffService.GetAllAsync(
                    cancellationToken);

            return Ok(staff);
        }

        // GET: api/dashboard/staff/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var staff =
                await _staffService.GetByIdAsync(
                    id,
                    cancellationToken);

            if (staff == null)
            {
                return NotFound(
                    $"Staff member with ID {id} does not exist.");
            }

            return Ok(staff);
        }
    }
}
