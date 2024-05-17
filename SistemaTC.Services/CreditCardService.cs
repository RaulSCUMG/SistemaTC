using Microsoft.EntityFrameworkCore;
using SistemaTC.Core;
using SistemaTC.Data;
using SistemaTC.Data.Entities;
using SistemaTC.Services.Interfaces;

namespace SistemaTC.Services;
public class CreditCardService(ITCContext dbContext) : ICreditCardService
{
    public async Task<List<CreditCard>> GetCreditCardsAsync()
    {
        return await dbContext.CreditCards.ToListAsync();
    }
    public async Task<CreditCard?> GetCreditCardAsync(Guid creditCardId)
    {
        return await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCardId);
    }
    public async Task<(CreditCard? creditCard, List<string> validationErrors)> AddAsync(CreditCard creditCard)
    {
        List<string> validationErrors;
        do
        {
            creditCard.Number = GenerarNumeroTarjeta();
            validationErrors = await ValidateUser(creditCard).ToListAsync();

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

        DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);
        creditCard.ExpirationDate = fechaActual.AddYears(2).AddMonths(6);
        creditCard.Expired = false;
        creditCard.Pin = (GenerarNumeroAleatorioTarjeta(1000, 9999)).Hash();
        creditCard.Ccv = GenerarNumeroAleatorioTarjeta(100, 999).Hash();
        creditCard.CreditAvailable = creditCard.CreditLimit;
        creditCard.ChargeRate = 36;
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
        var validationErrors = await ValidateUser(creditCard, false).ToListAsync();

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
        var validationErrors = await ValidateUser(creditCard, false).ToListAsync();

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

        await dbContext.SaveChangesAsync();

        return (entity, []);
    }
    public async Task<(CreditCard? creditCard, List<string> validationErrors)> UpdateLimiteCreditoAsync(CreditCard creditCard)
    {
        var validationErrors = await ValidateUser(creditCard, false).ToListAsync();

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
            entity.CreditAvailable = creditCard.CreditAvailable + diferencia;
        }

        await dbContext.SaveChangesAsync();

        return (entity, []);
    }

    private async IAsyncEnumerable<string> ValidateUser(CreditCard creditCard, bool newCreditCard = true)
    {
        var existingCreditCardCondition = dbContext.CreditCards.Where(x => x.Number == creditCard.Number);

        if (!newCreditCard)
            existingCreditCardCondition = existingCreditCardCondition.Where(x => x.CreditCardId != creditCard.CreditCardId);

        if (await existingCreditCardCondition.AnyAsync(x => x.Number == creditCard.Number))
        {
            yield return "Credit Card already exists";
        }

        if (!await dbContext.Users.AnyAsync(x => x.UserId == creditCard.UserId))
        {
            yield return "User doesn't exist";
        }
    }

    public static string GenerarNumeroTarjeta()
    {
        Random random = new Random();
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
        Random random = new Random();
        int resultado = random.Next(min, max + 1);
        return resultado.ToString();
    }
}
