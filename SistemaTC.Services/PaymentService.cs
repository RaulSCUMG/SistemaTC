using Microsoft.EntityFrameworkCore;
using SistemaTC.Core.Extensions;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;
using static SistemaTC.Core.Enums;

using Microsoft.Extensions.Logging;

namespace SistemaTC.Services;
public class PaymentService(TCContext dbContext, ILogger<CutoffService> logger) : IPaymentService
{
    public async Task<List<Payment>> GetPaymentsAsync()
    {
        return await dbContext.Payments.ToListAsync();
    }
    public async Task<Payment?> GetPaymentAsync(Guid paymentId)
    {
        return await dbContext.Payments.FirstOrDefaultAsync(x => x.PaymentId == paymentId);
    }
    public async Task<(Payment? payment, List<string> validationErrors)> AddAsync(Payment payment)
    {
        var hashTransaction = GetHashCode();
        var validationErrors = await ValidatePayment(payment).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        using (var paymentScope = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Guardar los cambios en la tarjeta de crédito
                await dbContext.Payments.AddAsync(payment);
                await dbContext.SaveChangesAsync();

                //Procedo a modificar el disponible de la tarjeta de credito
                var creditCard = await dbContext.CreditCards
                                                 .FirstOrDefaultAsync(x => x.CreditCardId == payment.CreditCardId);
                creditCard.CreditAvailable += payment.Amount;

                dbContext.CreditCards.Update(creditCard);
                await dbContext.SaveChangesAsync();

                await paymentScope.CommitAsync();

                return (payment, []);
            }
            catch (Exception e)
            {
                await paymentScope.RollbackAsync();
                var errorMessage = e.GetLastException();
                string msg = $"Error processing Credit Card Payment for credit card {payment.CreditCardId}. Transaction: {hashTransaction}. Error: {errorMessage}";
                logger.LogError(msg);

                validationErrors.Add(msg);
                return (null, validationErrors);
            }
        }
    }
    public async IAsyncEnumerable<string> ValidatePayment(Payment payment, bool newPayment = true)
    {
        var validaEstadoTarjeta = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == payment.CreditCardId);

        if (validaEstadoTarjeta is null)
        {
            yield return "Credit Card doesn't exist";
            yield break;
        }

        //Nueva Transaccion
        if (newPayment == true)
        {
            if (validaEstadoTarjeta.Active == false)
            {
                yield return "Inactive Card";
            }
        }
    }
}
