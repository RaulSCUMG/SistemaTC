using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface ICutoffService
{
    Task<List<CreditCutOff>> GetCreditCutoffAsync();
    Task<CreditCutOff?> GetCreditCutoffAsync(Guid creditCutoffId);
    Task CreateCreditCardsCutOff(string user);
}
