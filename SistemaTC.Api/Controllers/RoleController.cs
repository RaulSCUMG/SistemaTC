using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.Role;
using SistemaTC.Services.Interfaces;
using System.Net;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController(ILogger<RoleController> logger, IMapper mapper, IRoleService roleService) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Role>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<Role>>> Get()
    {
        try
        {
            logger.LogInformation("Getting roles...");
            var data = mapper.Map<List<Role>>(await roleService.GetRolesAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting roles. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
