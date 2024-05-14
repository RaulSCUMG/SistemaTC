using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.User;
using SistemaTC.Services.Interfaces;
using System.Net;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<User>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<User>>> Get()
    {
        try
        {
            logger.LogInformation("Getting users...");
            var data = mapper.Map<List<User>>(await userService.GetUsersAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting users. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
