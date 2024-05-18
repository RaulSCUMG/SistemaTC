using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface ICutoffService
{
    Task<List<CreditCutOff>> GetCreditCutoffAsync();
    Task<CreditCutOff?> GetCreditCutoffAsync(Guid creditCutoffId);
    Task<CreditCutOff?> GetCreditCutoffByDateAsync(Guid creditCardId, int year, int month);
    Task CreateCreditCardsCutOff(string user);
    Task<CreditCutOff?> GetLastCreditCutoffAsync(Guid creditCardId);
    decimal CalculateBalance(decimal lastBalance, decimal totalCredit, decimal totalDebit);
}
