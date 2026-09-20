using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatsAppService _whatsAppService;

        public WhatsAppController(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }
        [HttpPost("test")]
        public async Task<IActionResult> Test(
          [FromQuery] string phone,
          [FromQuery] string name,
          CancellationToken cancellationToken)
        {
            var result = await _whatsAppService
                .SendAppointmentConfirmationAsync(
                    phone,
                    name,
                    cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
