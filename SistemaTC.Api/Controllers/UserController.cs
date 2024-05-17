using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Api.Filters;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.Authentication;
using SistemaTC.DTO.User;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Permissions = SistemaTC.Core.General.Permissions;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService, ITokenService tokenService) : TCBaseController
{
    [HttpGet("")]
    [PermissionAuthorization(Permissions.VIEW_USER)]
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
    [PermissionAuthorization(Permissions.VIEW_USER)]
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

    [HttpPost("")]
    [PermissionAuthorization(Permissions.CREATE_USER)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User?>> Add([FromBody]NewUser user)
    {
        try
        {
            logger.LogInformation("Creating new user...");

            var requestData = mapper.Map<Data.Entities.User>(user);
            requestData.CreatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await userService.AddAsync(requestData);

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
    [PermissionAuthorization(Permissions.UPDATE_USER)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User?>> Update([FromBody] ExistingUser user)
    {
        try
        {
            logger.LogInformation("Updating user {UserId}...", user.UserId);

            var requestData = mapper.Map<Data.Entities.User>(user);
            requestData.UpdatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await userService.UpdateAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<User>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while updating user {userId}. Error: {message}", user.UserId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPatch("{userId}/Active")]
    [PermissionAuthorization(Permissions.INACTIVATE_USER)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(User))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<User?>> Inactivate(Guid userId, bool? active)
    {
        try
        {
            var validationErrors = new List<string>();

            if (userId == Guid.Empty) validationErrors.Add("User Id is required");
            if (!active.HasValue) validationErrors.Add("Active parameter is required");

            if(validationErrors.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, validationErrors);

            logger.LogInformation("Activating/Inactivating user {UserId}...", userId);

            var (entity, serviceValidationResult) = await userService.InactivateAsync(userId, active!.Value, LoggedInUser.UserName!);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<User>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while activating/inactivating user {userId}. Error: {message}", userId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AuthenticationResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<AuthenticationResponse?>> Authenticate([FromBody] AuthenticationRequest request)
    {
        try
        {
            logger.LogInformation("Getting user token {userName}...", request.UserName);
            var user = await userService.GetUserAsync(request.UserName, request.Password);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var tokenData = tokenService.CreateToken(user);

            AuthenticationResponse response = new() { 
                UserName = user.UserName,
                Token = tokenData
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting user token {userId}. Error: {message}", request.UserName, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
