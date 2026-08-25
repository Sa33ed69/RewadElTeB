using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/dashboard/contracts")]
    [Authorize(Roles = "Admin")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        // GET: api/dashboard/contracts
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var contracts = await _contractService
                .GetAllAsync(cancellationToken);

            return Ok(contracts);
        }

        // GET: api/dashboard/contracts/{id}
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var contract = await _contractService
                .GetByIdAsync(id, cancellationToken);

            if (contract == null)
            {
                return NotFound(new
                {
                    message = "Contract not found"
                });
            }

            return Ok(contract);
        }

        // POST: api/dashboard/contracts
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateContractDto dto,
            CancellationToken cancellationToken)
        {
            var contract = await _contractService
                .CreateAsync(dto, cancellationToken);

            return Ok(new
            {
                message = "Contract created successfully",
            });
        }

        // PUT: api/dashboard/contracts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateContractDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _contractService
                .UpdateAsync(id, dto, cancellationToken);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Contract not found"
                });
            }

            return Ok(new
            {
                message = "Contract updated successfully"
            });
        }

        // DELETE: api/dashboard/contracts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _contractService
                .DeleteAsync(id, cancellationToken);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Contract not found"
                });
            }

            return Ok(new
            {
                message = "Contract deleted successfully"
            });
        }
    }
}