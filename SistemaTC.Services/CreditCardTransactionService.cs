using Microsoft.EntityFrameworkCore;
using SistemaTC.Core.Extensions;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;
using static SistemaTC.Core.Enums;

using Microsoft.Extensions.Logging;
using SistemaTC.Core;

namespace SistemaTC.Services;

public class CreditCardTransactionService(TCContext dbContext, ILogger<CutoffService> logger) : ICreditCardTransactionService
{
    public async Task<List<CreditCardTransaction>> GetTransactionsAsync()
    {
        return await dbContext.CreditCardTransactions.ToListAsync();
    }
    public async Task<CreditCardTransaction?> GetTransactionAsync(Guid transactionId)
    {
        return await dbContext.CreditCardTransactions.FirstOrDefaultAsync(x => x.CreditCardTransactionId == transactionId);
    }
    public async Task<(CreditCardTransaction? transaction, List<string> validationErrors)> AddAsync(CreditCardTransaction transaction)
    {
        var hashTransaction = GetHashCode();
        var validationErrors = await ValidateTransaction(transaction).ToListAsync();
        var currentDate = DateTime.Now;

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        using (var transactionScope = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Guardar los cambios en la tarjeta de crédito
                await dbContext.CreditCardTransactions.AddAsync(transaction);
                await dbContext.SaveChangesAsync();

                //Procedo a modificar el disponible
                var creditCardInfo = await dbContext.CreditCards
                                                    .FirstOrDefaultAsync(x => x.CreditCardId == transaction.CreditCardId);
                if (creditCardInfo != null)
                {
                    if (transaction.Type == CreditCardTransactionType.Credit) //Consumo
                    {
                        creditCardInfo.CreditAvailable -= transaction.Amount;
                    }
                    else
                    {
                        if (transaction.Type == CreditCardTransactionType.Debit) //Pago
                        {
                            creditCardInfo.CreditAvailable += transaction.Amount;
                        }
                    }
                    dbContext.CreditCards.Update(creditCardInfo);
                    await dbContext.SaveChangesAsync();
                }
                else {
                    throw new Exception($"Credit card with ID {transaction.CreditCardId} not found.");
                }

                //Modificar el disponible del corte
                if (transaction.CreditCutOffId != null)
                {
                    var cutOffInfo = await dbContext.CreditCutOffs
                                                    .FirstOrDefaultAsync(x => x.CreditCardId == transaction.CreditCutOffId);
                    if (cutOffInfo != null)
                    {
                        if (transaction.Type == CreditCardTransactionType.Debit) //
                        {
                            cutOffInfo.PayedAmount += transaction.Amount;
                        }
                        dbContext.CreditCutOffs.Update(cutOffInfo);
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception($"Credit card CutOff with ID {currentDate.Year} - {currentDate.Month} not found.");
                    }
                }
                else
                {
                    throw new Exception($"Credit card with ID {transaction.CreditCardId} not found.");
                }

                await transactionScope.CommitAsync();

                return (transaction, []);
            }
            catch (Exception e)
            {
                await transactionScope.RollbackAsync();
                var errorMessage = e.GetLastException();
                string msg = $"Error processing Credit Card Transaction for credit card {transaction.CreditCardId}. Transaction: {hashTransaction}. Error: {errorMessage}";
                logger.LogError(msg);

                validationErrors.Add(msg);
                return (null, validationErrors);
            }
        }
    }
    public async IAsyncEnumerable<string> ValidateTransaction(CreditCardTransaction transaction, bool newTransaction = true)
    {
        var validaEstadoTarjeta = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == transaction.CreditCardId);

        if (validaEstadoTarjeta is null)
        {
            yield return "Credit Card doesn't exist";
            yield break;
        }

        decimal limiteActual = validaEstadoTarjeta.CreditAvailable;
        decimal consumo = transaction.Amount;
        decimal totalDisponible = limiteActual - consumo;

        //Nueva Transaccion
        if (newTransaction == true)
        {
            if (validaEstadoTarjeta.Active == false)
            {
                yield return "Inactive Card";
            }
            if (validaEstadoTarjeta.Locked == true)
            {
                yield return "Locked card";
            }
            if (totalDisponible < 0)
            {
                yield return "Insufficient balance";
            }
            if (transaction.CreditCutOffId == null)
            {
                yield return "Credit cut off not exist";
            }
        }
    }
}
