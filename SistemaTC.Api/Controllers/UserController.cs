using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.User;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
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

    [HttpGet("{userId}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User>> Get(Guid userId)
    {
        try
        {
            logger.LogInformation("Getting user {userId}...", userId);
            var data = mapper.Map<User>(await userService.GetUserAsync(userId));

            if (data == null)
            {
                return BadRequest("User not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting user {userId}. Error: {message}", userId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("ByUserName")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User?>> Get([FromBody] UserRequest request)
    {
        try
        {
            logger.LogInformation("Getting user {userName}...", request.UserName);
            var data = mapper.Map<User>(await userService.GetUserAsync(request.UserName, request.Password));

            if(data == null)
            {
                return BadRequest("User not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting user {userId}. Error: {message}", request.UserName, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User?>> Add([FromBody]NewUser user)
    {
        try
        {
            logger.LogInformation("Creating new user...");

            var (entity, serviceValidationResult) = await userService.AddAsync(mapper.Map<Data.Entities.User>(user));

            if(serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<User>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while creating the user. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPut("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User?>> Update([FromBody] ExistingUser user)
    {
        try
        {
            logger.LogInformation("Updating user {UserId}...", user.UserId);

            var (entity, serviceValidationResult) = await userService.UpdateAsync(mapper.Map<Data.Entities.User>(user));

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<User>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while updating the user. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
