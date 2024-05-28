using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Api.Filters;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.Payment;
using SistemaTC.DTO.User;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Permissions = SistemaTC.Core.General.Permissions;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController(ILogger<PaymentController> logger, IMapper mapper, IPaymentService paymentService) : TCBaseController
{
    [HttpGet("")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD_PAYMENT)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Payment>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<Payment>>> GetPayment()
    {
        try
        {
            logger.LogInformation("Getting payment...");
            var data = mapper.Map<List<Payment>>(await paymentService.GetPaymentsAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting payment. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{paymentId}")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD_PAYMENT)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Payment))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Payment>> GetTransaction(Guid paymentId)
    {
        try
        {
            logger.LogInformation("Getting payment {paymentId}...", paymentId);
            var data = mapper.Map<Payment>(await paymentService.GetPaymentAsync(paymentId));

            if (data == null)
            {
                return BadRequest("Payment not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting payment {transactionId}. Error: {message}", paymentId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("")]
    [PermissionAuthorization(Permissions.CREATE_CREDIT_CARD_PAYMENT)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Payment))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<Payment?>> Add([FromBody] PaymentNew payment)
    {
        try
        {
            logger.LogInformation("Creating new payment...");
            var requestData = mapper.Map<Data.Entities.Payment>(payment);
            requestData.CreatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await paymentService.AddAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<Payment>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while creating the payment. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
