using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Api.Filters;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.CreditCard;
using SistemaTC.DTO.User;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Permissions = SistemaTC.Core.General.Permissions;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CreditCardController(ILogger<CreditCardController> logger, IMapper mapper, ICreditCardService creditCardService, ICutoffService cutoffService) : TCBaseController
{
    [HttpGet("")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CreditCard>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<List<CreditCard>>> GetTarjetas()
    {
        try
        {
            logger.LogInformation("Getting Credit Cards...");
            var data = mapper.Map<List<CreditCard>>(await creditCardService.GetCreditCardsAsync());
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit cards. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{creditCardId}")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard>> GetTarjeta(Guid creditCardId)
    {
        try
        {
            logger.LogInformation("Getting credit card {creditCardId}...", creditCardId);
            var data = mapper.Map<CreditCard>(await creditCardService.GetCreditCardAsync(creditCardId));

            if (data == null)
            {
                return BadRequest("Credit Card not found");
            }

            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit card {creditCardId}. Error: {message}", creditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{creditCardId}/saldo")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD_CUTOFF)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard>> GetTarjetaSaldos(Guid creditCardId)
    {
        try
        {
            logger.LogInformation("Getting credit card {creditCardId}...", creditCardId);
            var creditCard = await creditCardService.GetCreditCardAsync(creditCardId);

            if (creditCard == null)
            {
                return BadRequest("Credit Card not found");
            }

            var lastCreditCutoff = await cutoffService.GetLastCreditCutoffAsync(creditCard.CreditCardId);
            var (totalCredit, totalDebit) = await creditCardService.GetCurrentCreditCardSumTransactionsAsync(creditCard.CreditCardId);

            var response = new CreditCardResponseSaldo
            {
                CreditCardId = creditCard.CreditCardId,
                CreditAvailable = creditCard.CreditAvailable,
                CurrentBalance = cutoffService.CalculateBalance(lastCreditCutoff?.TotalBalance ?? 0, totalCredit, totalDebit),
                BalanceAtCutOff = lastCreditCutoff?.TotalBalance ?? 0
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit card {creditCardId}. Error: {message}", creditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{creditCardId}/fechaCorte")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard>> GetTarjetaFechaCorte(Guid creditCardId)
    {
        try
        {
            logger.LogInformation("Getting credit card {creditCardId}...", creditCardId);
            var data = mapper.Map<CreditCard>(await creditCardService.GetCreditCardAsync(creditCardId));

            if (data == null)
            {
                return BadRequest("Credit Card not found");
            }

            var response = new CreditCardResponseFecha
            {
                CreditCardId = data.CreditCardId,
                PreviousBalanceCutOffDate = data.NextBalanceCutOffDate.AddMonths(-1),
                NextBalanceCutOffDate = data.NextBalanceCutOffDate.AddMonths(1)
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit card {creditCardId}. Error: {message}", creditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpGet("{creditCardId}/TarjetaDetalle")]
    [PermissionAuthorization(Permissions.VIEW_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard>> GetTarjetaDetalle(Guid creditCardId)
    {
        try
        {
            logger.LogInformation("Getting credit card {creditCardId}...", creditCardId);
            var data = mapper.Map<CreditCard>(await creditCardService.GetCreditCardAsync(creditCardId));

            if (data == null)
            {
                return BadRequest("Credit Card not found");
            }

            var response = new CreditCardResponseDetalle
            {
                CreditCardId = data.CreditCardId,
                CreditAvailable = data.CreditAvailable,
                NextPaymentDate = data.NextPaymentDate,
                ChargeRate = data.ChargeRate,
                NextBalanceCutOffDate = data.NextBalanceCutOffDate,
                //En lo que se finaliza el controller de los cortes para recuperar esta informacion
                CurrentBalance = 0,
                BalanceAtCutOff = 0
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced on getting credit card {creditCardId}. Error: {message}", creditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPost("")]
    [PermissionAuthorization(Permissions.CREATE_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> Add([FromBody] CreditCardNew creditCard)
    {
        try
        {
            logger.LogInformation("Creating new Credit Card...");
            var requestData = mapper.Map<Data.Entities.CreditCard>(creditCard);
            requestData.CreatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await creditCardService.AddAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<CreditCard>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while creating the Credit Card. Error: {message}", message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPut("pin")]
    [PermissionAuthorization(Permissions.UPDATE_PIN_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> UpdatePin([FromBody] ExistingCreditCardPin creditCard)
    {
        try
        {
            logger.LogInformation("Updating credit card pin {CreditCardId}...", creditCard.CreditCardId);

            var requestData = mapper.Map<Data.Entities.CreditCard>(creditCard);
            requestData.UpdatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await creditCardService.UpdatePinAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<CreditCard>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while updating credit card pin {CreditCardId}. Error: {message}", creditCard.CreditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPut("bloqueo")]
    [PermissionAuthorization(Permissions.UPDATE_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> UpdateBlock([FromBody] ExistingCreditCardBloqueo creditCard)
    {
        try
        {
            logger.LogInformation("Updating credit card block {CreditCardId}...", creditCard.CreditCardId);

            var requestData = mapper.Map<Data.Entities.CreditCard>(creditCard);
            requestData.UpdatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await creditCardService.UpdateBloqueoAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<CreditCard>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while updating credit card block {CreditCardId}. Error: {message}", creditCard.CreditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }

    [HttpPut("limiteCredito")]
    [PermissionAuthorization(Permissions.UPDATE_CREDIT_CARD)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> UpdateIncreaseCreditLimit([FromBody] ExistingCreditCardAumento creditCard)
    {
        try
        {
            logger.LogInformation("Updating increase credit limit {CreditCardId}...", creditCard.CreditCardId);

            var requestData = mapper.Map<Data.Entities.CreditCard>(creditCard);
            requestData.UpdatedBy = LoggedInUser.UserName!;

            var (entity, serviceValidationResult) = await creditCardService.UpdateLimiteCreditoAsync(requestData);

            if (serviceValidationResult.Count is not 0)
                return StatusCode((int)HttpStatusCode.UnprocessableContent, serviceValidationResult);

            var data = mapper.Map<CreditCard>(entity);
            return Ok(data);
        }
        catch (Exception e)
        {
            var message = e.GetLastException();
            logger.LogError("Error produced while updating increase credit limit {CreditCardId}. Error: {message}", creditCard.CreditCardId, message);
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
