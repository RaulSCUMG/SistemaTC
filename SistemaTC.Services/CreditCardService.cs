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
        var validationErrors = await ValidateUser(creditCard).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        await dbContext.CreditCards.AddAsync(creditCard);
        await dbContext.SaveChangesAsync();

        return (creditCard, []);
    }
    public async Task<(CreditCard? creditCard, List<string> validationErrors)> UpdateAsync(CreditCard creditCard)
    {
        var validationErrors = await ValidateUser(creditCard, false).ToListAsync();

        if (validationErrors.Count is not 0)
        {
            return (null, validationErrors);
        }

        var entity = await dbContext.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == creditCard.CreditCardId);

        if (entity is null)
            return (null, ["Credit Card doesn't exist"]);

        entity.Updated = creditCard.Updated;
        entity.UpdatedBy = creditCard.UpdatedBy;

        if (!string.IsNullOrEmpty(creditCard.Pin))
            entity.Pin = creditCard.Pin;

        if (entity.Locked != creditCard.Locked)
        {
            entity.Locked = creditCard.Locked;
            entity.LockedDate = creditCard.LockedDate;
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
}
