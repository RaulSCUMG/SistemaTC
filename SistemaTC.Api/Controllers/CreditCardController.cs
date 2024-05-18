using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaTC.Core.Extensions;
using SistemaTC.DTO.CreditCard;
using SistemaTC.DTO.User;
using SistemaTC.Services;
using SistemaTC.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SistemaTC.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CreditCardController(ILogger<CreditCardController> logger, IMapper mapper, ICreditCardService creditCardService) : ControllerBase
{
    [HttpGet("")]
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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard>> GetTarjetaSaldos(Guid creditCardId)
    {
        try
        {
            logger.LogInformation("Getting credit card {creditCardId}...", creditCardId);
            var data = mapper.Map<CreditCard>(await creditCardService.GetCreditCardAsync(creditCardId));

            if (data == null)
            {
                return BadRequest("Credit Card not found");
            }

            var response = new CreditCardResponseSaldo
            {
                CreditCardId = data.CreditCardId,
                CreditAvailable = data.CreditAvailable,
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

    [HttpGet("{creditCardId}/fechaCorte")]
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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> Add([FromBody] CreditCardNew creditCard)
    {
        try
        {
            logger.LogInformation("Creating new Credit Card...");

            var (entity, serviceValidationResult) = await creditCardService.AddAsync(mapper.Map<Data.Entities.CreditCard>(creditCard));

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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> UpdatePin([FromBody] ExistingCreditCardPin creditCard)
    {
        try
        {
            logger.LogInformation("Updating credit card pin {CreditCardId}...", creditCard.CreditCardId);

            var (entity, serviceValidationResult) = await creditCardService.UpdatePinAsync(mapper.Map<Data.Entities.CreditCard>(creditCard));

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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> UpdateBlock([FromBody] ExistingCreditCardBloqueo creditCard)
    {
        try
        {
            logger.LogInformation("Updating credit card block {CreditCardId}...", creditCard.CreditCardId);

            var (entity, serviceValidationResult) = await creditCardService.UpdateBloqueoAsync(mapper.Map<Data.Entities.CreditCard>(creditCard));

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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreditCard))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ICollection<ValidationResult>))]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableContent, Type = typeof(List<string>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
    public async Task<ActionResult<CreditCard?>> UpdateIncreaseCreditLimit([FromBody] ExistingCreditCardAumento creditCard)
    {
        try
        {
            logger.LogInformation("Updating increase credit limit {CreditCardId}...", creditCard.CreditCardId);

            var (entity, serviceValidationResult) = await creditCardService.UpdateLimiteCreditoAsync(mapper.Map<Data.Entities.CreditCard>(creditCard));

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
