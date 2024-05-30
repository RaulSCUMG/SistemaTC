using Microsoft.EntityFrameworkCore;
using SistemaTC.Core.Extensions;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;
using static SistemaTC.Core.Enums;

using Microsoft.Extensions.Logging;
using System.IO.Pipes;
using System.Runtime.CompilerServices;

namespace SistemaTC.Services;
public class PaymentService(TCContext dbContext, ILogger<CutoffService> logger, ICreditCardTransactionService ccTransactionService) : IPaymentService
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

        try
        {
            // Guardar los cambios en la tarjeta de crédito
            var cutOffInfo = await dbContext.CreditCutOffs
                                            .FirstOrDefaultAsync(x => x.CreditCutOffId == payment.CreditCutOffId);
            decimal balanceTotal = cutOffInfo.TotalBalance;
            decimal payedAmount = cutOffInfo.PayedAmount + payment.Amount;

            if (payedAmount >= balanceTotal)
            {
                payment.Type = PaymentType.Cash;
            }
            else
            {
                payment.Type = PaymentType.Partial;
            }

            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();

            // Crear una transacción de tarjeta de crédito basada en el pago
            var creditCardTransaction = new CreditCardTransaction
            {
                UserId = payment.UserId,
                CreditCardId = payment.CreditCardId,
                CreditCutOffId = payment.CreditCutOffId,
                Type = CreditCardTransactionType.Debit, // O tipo que necesites
                Description = "Pago Recibido",
                Amount = payment.Amount
            };

            var (transaction, transactionValidationErrors) = await ccTransactionService.AddAsync(creditCardTransaction);

            if (transactionValidationErrors.Count != 0)
            {
                return (null, transactionValidationErrors);
            }

            return (payment, []);
        }
        catch (Exception e)
        {
            var errorMessage = e.GetLastException();
            string msg = $"Error processing Credit Card Payment for credit card {payment.CreditCardId}. Transaction: {hashTransaction}. Error: {errorMessage}";
            logger.LogError(msg);

            validationErrors.Add(msg);
            return (null, validationErrors);
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
