using Microsoft.AspNetCore.Mvc;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.Notification;
using SistemaTC.Services.Interfaces;
using System.Net;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotificationController(ILogger<NotificationController> logger, INotificationService notificationService) : ControllerBase
{
    [HttpPost("email")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<bool>> SendEmail([FromBody] EmailData data)
    {
        try
        {
            logger.LogInformation("Sending email...");
            var success = await notificationService.SendEmail(data.To, data.Subject, data.Body, data.IsHtml);

            if(success)
            {
                return Ok(success);
            }

            return StatusCode((int)HttpStatusCode.UnprocessableContent, "Mail failed to send. Contact your administrator for support.");
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced sending the email. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
