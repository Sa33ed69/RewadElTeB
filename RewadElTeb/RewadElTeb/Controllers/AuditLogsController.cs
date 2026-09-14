using Application.Interfaces.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RewadElTeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogsController(
            IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
                    CancellationToken cancellationToken)
        {
            var result = await _auditLogService
                .GetAllAsync(cancellationToken);

            return Ok(result);
        }
    }
}
