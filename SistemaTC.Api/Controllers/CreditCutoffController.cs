using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Core.Extensions;
using SistemaTC.Api.Filters;
using SistemaTC.DTO.CreditCutoff;
using SistemaTC.Services.Interfaces;
using static SistemaTC.Core.General;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CreditCutoffController(ILogger<CreditCutoffController> logger, IMapper mapper, ICutoffService creditCutoffService) : TCBaseController
{
    [HttpGet("")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD_CUTOFF)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CreditCutoff>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<CreditCutoff>>> Get()
    {
        try
        {
            logger.LogInformation("Getting credit cutoffs...");
            var data = mapper.Map<List<CreditCutoff>>(await creditCutoffService.GetCreditCutoffAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit cutoffs. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{creditCutoffId}")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD_CUTOFF)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCutoff))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCutoff>> Get(Guid creditCutoffId)
    {
        try
        {
            logger.LogInformation("Getting credit cutoff {creditCutoffId}...", creditCutoffId);
            var data = mapper.Map<CreditCutoff>(await creditCutoffService.GetCreditCutoffAsync(creditCutoffId));

            if (data == null)
            {
                return BadRequest("Credit Cutoff not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit cutoff {creditCutoffId}. Error: {message}", creditCutoffId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("")]
    [PermissionAuthorization(Permissions.CREATE_CREDIT_CARD_CUTOFF)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCutoff))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCutoff?>> Add()
    {
        try
        {
            logger.LogInformation("Creating credit cards cutoff...");

            await creditCutoffService.CreateCreditCardsCutOff(LoggedInUser.UserName!);
            return Ok();
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while creating the credit cards cutoff. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
