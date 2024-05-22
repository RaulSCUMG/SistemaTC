using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Api.Filters;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.CreditCardTransaction;
using SistemaTC.Services;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Permissions = SistemaTC.Core.General.Permissions;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CreditCardTransactionController(ILogger<CreditCardController> logger, IMapper mapper, ICreditCardTransactionService transactionService) : TCBaseController
{
    [HttpGet("")]
    [PermissionAuthorization(Permissions.CREATE_CC_TRANSACTION)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CreditCardTransaction>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<CreditCardTransaction>>> GetTransactions()
    {
        try
        {
            logger.LogInformation("Getting Credit Card Transactions...");
            var data = mapper.Map<List<CreditCardTransaction>>(await transactionService.GetTransactionsAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit card transactions. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{transactionId}")]
    [PermissionAuthorization(Permissions.CREATE_CC_TRANSACTION)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCardTransaction))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCardTransaction>> GetTransaction(Guid transactionId)
    {
        try
        {
            logger.LogInformation("Getting credit card transaction {transactionId}...", transactionId);
            var data = mapper.Map<CreditCardTransaction>(await transactionService.GetTransactionAsync(transactionId));

            if (data == null)
            {
                return BadRequest("Credit Card Transaction not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit card transaction {transactionId}. Error: {message}", transactionId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("")]
    [PermissionAuthorization(Permissions.CREATE_CC_TRANSACTION)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCardTransaction))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCardTransaction?>> Add([FromBody] CreditCardTransactionNew creditCard)
    {
        try
        {
            logger.LogInformation("Creating new Credit Card Transaction...");
            var requestData = mapper.Map<Data.Entities.CreditCardTransaction>(creditCard);
            requestData.CreatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await transactionService.AddAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<CreditCardTransaction>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while creating the Credit Card Transaction. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}