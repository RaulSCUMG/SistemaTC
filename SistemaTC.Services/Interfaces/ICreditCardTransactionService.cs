using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;

public interface ICreditCardTransactionService
{
    Task<List<CreditCardTransaction>> GetTransactionsAsync();
    Task<CreditCardTransaction?> GetTransactionAsync(Guid transactionId);
    Task<(CreditCardTransaction? transaction, List<string> validationErrors)> AddAsync(CreditCardTransaction transaction);
    IAsyncEnumerable<string> ValidateTransaction(CreditCardTransaction transaction, bool newTransaction = true);
}
