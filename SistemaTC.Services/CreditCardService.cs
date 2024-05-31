﻿using Microsoft.EntityFrameworkCore;
using SistemaTC.Core.Extensions;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;
using static SistemaTC.Core.Enums;

namespace SistemaTC.Services;
public class CreditCardService(ITCContext dbContext) : ICreditCardService
{
    public async Task<List<CreditCard>> GetCreditCardsAsync()
    {
        return await dbContext.CreditCards.ToListAsync();
    }
    public async Task<CreditCard?> GetCreditCardAsync(Guid creditCardId)
    {
        return await dbContext.CreditCards
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
    }
    public async Task<(decimal totalCredit, decimal totalDebit)> GetCurrentCreditCardSumTransactionsAsync(Guid creditCardId)
    {
        var transactions = await dbContext.CreditCardTransactions
            .Where(x => x.CreditCardId == creditCardId
                    && x.CreditCutOffId == null)
            .ToListAsync();

        return (transactions.Where(x => x.Type == CreditCardTransactionType.Credit).Sum(x => x.Amount),
            transactions.Where(x => x.Type == CreditCardTransactionType.Debit).Sum(x => x.Amount));
    }
    public async Task<List<CreditCardTransaction>> GetCreditCardTransactionsAsync(Guid creditCutOffId)
    {
        return await dbContext.CreditCardTransactions
            .Where(x => x.CreditCutOffId == creditCutOffId)
            .ToListAsync();
    }
    public async Task<List<CreditCardTransaction>> GetCreditCardAccountStatusAsync(DateTime creditCutOffDate, Guid creditCardId)
    {
        var cutOffMonth = creditCutOffDate.Month;
        var cutOffYear = creditCutOffDate.Year;
        
        return await dbContext.CreditCardTransactions
                                .Join(dbContext.CreditCutOffs,
                                      transaction => transaction.CreditCutOffId,
                                      cutOff => cutOff.CreditCutOffId,
                                      (transaction, cutOff) => new { Transaction = transaction, CutOff = cutOff })
                                .Where(joined => joined.CutOff.Month == cutOffMonth && 
                                                 joined.CutOff.Year == cutOffYear &&
                                                 joined.Transaction.CreditCardId == creditCardId)
                                .Select(joined => joined.Transaction)
                                .ToListAsync();
    }
    public async Task<(CreditCard? creditCard, List<string> validationErrors)> AddAsync(CreditCard creditCard)
    {
        List<string> validationErrors;
        do
        {
            creditCard.Number = GenerarNumeroTarjeta();
            validationErrors = await ValidateCreditCard(creditCard).ToListAsync();

            if (validationErrors.Contains("Credit Card already exists"))
            {
                // Si ya existe una tarjeta con el mismo número, genera uno nuevo y vuelve a validar
                continue;
            }

            if (validationErrors.Count > 0)
            {
                // Si hay otros errores de validación, devuelve los errores
                return (null, validationErrors);
            }

            // Si no hay errores, guarda la tarjeta y termina el bucle
            
            break;
        } while (true);

        DateTime fechaActual = DateTime.Today;
        string valPin = (GenerarNumeroAleatorioTarjeta(1000, 9999));
        string valCcv = GenerarNumeroAleatorioTarjeta(100, 999);
        creditCard.ExpirationDate = fechaActual.AddYears(2).AddMonths(6);
        creditCard.Expired = false;
        creditCard.Pin = valPin;
        creditCard.Ccv = valCcv;
        creditCard.CreditAvailable = creditCard.CreditLimit;
        creditCard.BalanceCutOffDay = (fechaActual.AddDays(15)).Day;
        creditCard.NextBalanceCutOffDate = fechaActual.AddDays(15).AddMonths(1);
        creditCard.PaymentDay = (fechaActual.AddDays(10)).Day;
        creditCard.NextPaymentDate = fechaActual.AddDays(10).AddMonths(1);
        creditCard.Active = true;
        creditCard.ActivationDate = DateTime.Today;
        creditCard.Locked = false;
        creditCard.LockedDate = null;

        await dbContext.CreditCards.AddAsync(creditCard);
        await dbContext.SaveChangesAsync();

        return (creditCard, []);
    }
    public async Task<(CreditCard? creditCard, List<string> validationErrors)> UpdatePinAsync(CreditCard creditCard)
    {
        var validationErrors = await ValidateCreditCard(creditCard, false).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        var entity = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCard.CreditCardId);

        if (entity is null)
            return (null, ["Credit Card doesn't exist"]);

        if (!string.IsNullOrEmpty(creditCard.Pin))
            entity.Pin = creditCard.Pin;

        await dbContext.SaveChangesAsync();

        return (entity, []);
    }

    public async Task<(CreditCard? creditCard, List<string> validationErrors)> UpdateBloqueoAsync(CreditCard creditCard)
    {
        var validationErrors = await ValidateCreditCard(creditCard, false).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        var entity = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCard.CreditCardId);

        if (entity is null)
            return (null, ["Credit Card doesn't exist"]);

        if (entity.Locked != creditCard.Locked)
        {
            entity.Locked = creditCard.Locked;
        }
        entity.LockedDate = creditCard.LockedDate;

        await dbContext.SaveChangesAsync();

        return (entity, []);
    }
    public async Task<(CreditCard? creditCard, List<string> validationErrors)> UpdateLimiteCreditoAsync(CreditCard creditCard)
    {
        var validationErrors = await ValidateCreditCard(creditCard, false).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        var entity = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCard.CreditCardId);

        if (entity is null)
            return (null, ["Credit Card doesn't exist"]);

        if (entity.CreditLimit != creditCard.CreditLimit)
        {
            decimal diferencia = (creditCard.CreditLimit - entity.CreditLimit);
            entity.CreditLimit = creditCard.CreditLimit;
            entity.CreditAvailable = entity.CreditAvailable + diferencia;
        }

        await dbContext.SaveChangesAsync();

        return (entity, []);
    }

    public async IAsyncEnumerable<string> ValidateCreditCard(CreditCard creditCard, bool newCreditCard = true)
    {
        var existingCreditCardCondition = dbContext.CreditCards.Where(x => x.Number == creditCard.Number);

        if (!newCreditCard)
            existingCreditCardCondition = existingCreditCardCondition.Where(x => x.CreditCardId != creditCard.CreditCardId);

        if (await existingCreditCardCondition.AnyAsync(x => x.Number == creditCard.Number))
        {
            yield return "Credit Card already exists";
        }

        if (newCreditCard == false)
        {
            var creditCardIfo = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCard.CreditCardId);
            if (!await dbContext.Users.AnyAsync(x => x.UserId == creditCardIfo.UserId))
            {
                yield return "User doesn't exist";
            }
        }
    }

    public static string GenerarNumeroTarjeta()
    {
        Random random = new();
        int[] cardNumber = new int[16];

        cardNumber[0] = random.Next(4, 5);

        for (int i = 1; i < 15; i++)
        {
            cardNumber[i] = random.Next(0, 10);
        }

        int sum = 0;
        bool doubleDigit = false;
        for (int i = 14; i >= 0; i--)
        {
            int digit = cardNumber[i];
            if (doubleDigit)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }
            sum += digit;
            doubleDigit = !doubleDigit;
        }

        int checkDigit = (sum * 9) % 10;
        cardNumber[15] = checkDigit;

        string cardNumberStr = string.Join("", cardNumber);
        return cardNumberStr;
    }

    public static string GenerarNumeroAleatorioTarjeta(int min, int max)
    {
        Random random = new();
        int resultado = random.Next(min, max + 1);
        return resultado.ToString();
    }
}
