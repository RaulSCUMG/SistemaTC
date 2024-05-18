using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Api.Filters;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.Request;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Permissions = SistemaTC.Core.General.Permissions;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestController(ILogger<RequestController> logger, IMapper mapper, IRequestService requestService, ICreditCardService creditCardService) : TCBaseController
{
    [HttpGet("")]
    [PermissionAuthorization(Permissions.VIEW_REQUEST)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Request>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<Request>>> Get()
    {
        try
        {
            logger.LogInformation("Getting requests...");
            var data = mapper.Map<List<Request>>(await requestService.GetRequestsAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting requests. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{requestId}")]
    [PermissionAuthorization(Permissions.VIEW_REQUEST)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Request))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Request>> Get(Guid requestId)
    {
        try
        {
            logger.LogInformation("Getting request {requestId}...", requestId);
            var data = mapper.Map<Request>(await requestService.GetRequestAsync(requestId));

            if (data == null)
            {
                return BadRequest("Request not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting request {requestId}. Error: {message}", requestId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("")]
    [PermissionAuthorization(Permissions.CREATE_REQUEST)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Request))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Request?>> Add([FromBody] NewRequest request)
    {
        try
        {
            logger.LogInformation("Creating new request...");
            var requestData = mapper.Map<Data.Entities.Request>(request);
            requestData.CreatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await requestService.AddAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<Request>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while creating the request. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPut("NewCreditCard")]
    [PermissionAuthorization(Permissions.PROCESS_REQUEST)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Request))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Request?>> ProcessCreditCardRequest([FromBody] NewCreditCardRequest request)
    {
        try
        {
            logger.LogInformation("Processing new credit card request {RequestId}...", request.RequestId);
            var requestData = mapper.Map<Data.Entities.Request>(request);
            requestData.UpdatedBy = LoggedInUser.UserName!;

            var requestServiceValidationResult = await requestService.ValidateRequest(requestData, false).ToListAsync();

            if (requestServiceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, requestServiceValidationResult);

            var requestEntity = (await requestService.GetRequestAsync(request.RequestId));

            var creditCardData = mapper.Map<Data.Entities.CreditCard>(request);
            creditCardData.UpdatedBy = LoggedInUser.UserName!;
            creditCardData.UserId = requestEntity!.RequestedByUserId;

            var creditCardServiceValidationResult = await creditCardService.ValidateCreditCard(creditCardData).ToListAsync();

            if (creditCardServiceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, creditCardServiceValidationResult);

            if(request.Approved)
                _ = await creditCardService.AddAsync(creditCardData);

            var entity = await requestService.ProcessRequest(requestData);

            var data = mapper.Map<Request>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while processing the new credit card request. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
