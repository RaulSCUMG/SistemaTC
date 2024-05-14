using Microsoft.AspNetCore.Mvc;

namespace SistemaTC.Api.Controllers;
[ApiController]
[Route("api/health-check")]
public class HealthCheckController(ILogger<HealthCheckController> logger) : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get()
    {
        logger.LogInformation("Executing health check...");
        return Ok(new { response = "success" });
    }
}
