using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface ICreditCardService
{
    Task<List<CreditCard>> GetCreditCardsAsync();
    Task<CreditCard?> GetCreditCardAsync(Guid creditCardId);
    Task<(CreditCard? creditCard, List<string> validationErrors)> AddAsync(CreditCard creditCard);
    Task<(CreditCard? creditCard, List<string> validationErrors)> UpdatePinAsync(CreditCard creditCard);
    Task<(CreditCard? creditCard, List<string> validationErrors)> UpdateBloqueoAsync(CreditCard creditCard);
    Task<(CreditCard? creditCard, List<string> validationErrors)> UpdateLimiteCreditoAsync(CreditCard creditCard);
}
