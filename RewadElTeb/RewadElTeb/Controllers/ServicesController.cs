using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [ApiController]
    [Route("api/dashboard/services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // CREATE
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromForm] CreateServiceDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _serviceService.CreateAsync(
                dto,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // UPDATE
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateServiceDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _serviceService.UpdateAsync(
                id,
                dto,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _serviceService.DeleteAsync(
                id,
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // GET ALL
        [HttpGet]
     
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var services = await _serviceService.GetAllAsync(
                cancellationToken);

            return Ok(new
            {
                data = services,
                isSuccess = true,
                message = (string?)null
            });
        }

        // GET BY ID
        [HttpGet("{id:int}")]
      
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var service = await _serviceService.GetByIdAsync(
                id,
                cancellationToken);

            if (service == null)
            {
                return NotFound(new
                {
                    data = (object?)null,
                    isSuccess = false,
                    message = $"Service with ID {id} does not exist."
                });
            }

            return Ok(new
            {
                data = service,
                isSuccess = true,
                message = (string?)null
            });
        }
    }
}