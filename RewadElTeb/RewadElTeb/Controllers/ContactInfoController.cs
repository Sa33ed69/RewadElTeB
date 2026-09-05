using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoService _contactInfoService;

        public ContactInfoController(
            IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken)
        {
            var result = await _contactInfoService
                .GetAsync(cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateContactInfoDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _contactInfoService
                .CreateAsync(dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
                 int id,
                 [FromBody] UpdateContactInfoDto dto,
                 CancellationToken cancellationToken)
        {
            var result = await _contactInfoService
                .UpdateAsync(id, dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
                int id,
                CancellationToken cancellationToken)
        {
            var result = await _contactInfoService
                .DeleteAsync(id, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
