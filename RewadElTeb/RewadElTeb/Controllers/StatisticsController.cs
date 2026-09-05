using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(
            IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("Get_All_Statistics")]
        
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        { 
            var result = await _statisticService.GetAllAsync(cancellationToken);

            if (!result.IsSuccess)
            {
             return BadRequest(new { message = result.Message });
            }
            
            return Ok(result.Data); 
        }
        [HttpGet("Get_By_Id_Statistics/{id:int}")]
        
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) 
        {
            var result = await _statisticService.GetByIdAsync(id, cancellationToken);

            if (!result.IsSuccess) 
            {
                return NotFound(new { message = result.Message });
            }

            return Ok(result.Data); 
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create_Statistics")]
        public async Task<IActionResult> Create([FromBody] CreateStatisticDto dto, CancellationToken cancellationToken) 
        { 
            var result = await _statisticService.CreateAsync(dto, cancellationToken); 

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = "Statistic created successfully" }); 
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update_Statistics/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStatisticDto dto, CancellationToken cancellationToken)
        {
            var result = await _statisticService.UpdateAsync(id, dto, cancellationToken);

            if (!result.IsSuccess)
            {
                if (result.Message == "Statistic not found") 
                { 
                    return NotFound(new { message = result.Message });
                }
                return BadRequest(new { message = result.Message });

            }
            return Ok(new { message = result.Message }); 
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete_Statistics/{id:int}")]
        public async Task<IActionResult> Delete( int id, CancellationToken cancellationToken)
        { 
            var result = await _statisticService .DeleteAsync(id, cancellationToken);

            if (!result.IsSuccess) 
            {
                return NotFound(new { message = result.Message }); 
            }
            return Ok(new { message = result.Message });
        }
    }
}
