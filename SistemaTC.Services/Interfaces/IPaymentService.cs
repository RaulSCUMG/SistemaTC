using SistemaTC.Data.Entities;

namespace SistemaTC.Services.Interfaces;
public interface IPaymentService
{
    Task<List<Payment>> GetPaymentsAsync();
    Task<Payment?> GetPaymentAsync(Guid paymentId);
    Task<(Payment? payment, List<string> validationErrors)> AddAsync(Payment payment);
    IAsyncEnumerable<string> ValidatePayment(Payment payment, bool newPayment = true);
}
