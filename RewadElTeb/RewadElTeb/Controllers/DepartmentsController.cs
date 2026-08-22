using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [ApiController]
    [Route("api/dashboard/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // CREATE
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateDepartmentDto dto,
            CancellationToken cancellationToken)
        {
            var result =
                await _departmentService.CreateAsync(
                    dto,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Roles = "Admin")]
        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateDepartmentDto dto,
            CancellationToken cancellationToken)
        {
            var result =
                await _departmentService.UpdateAsync(
                    id,
                    dto,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // DELETE
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result =
                await _departmentService.DeleteAsync(
                    id,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var departments =
                await _departmentService.GetAllAsync(
                    cancellationToken);

            return Ok(departments);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var department =
                await _departmentService.GetByIdAsync(
                    id,
                    cancellationToken);

            if (department == null)
            {
                return NotFound(
                    $"Department with ID {id} does not exist.");
            }

            return Ok(department);
        }

        [HttpGet("with-doctors")]
        public async Task<IActionResult> GetAllWithDoctors(
            CancellationToken cancellationToken)
        {
            var departments =
                await _departmentService.GetAllWithDoctorsAsync(
                    cancellationToken);

            return Ok(departments);
        }
    }
}