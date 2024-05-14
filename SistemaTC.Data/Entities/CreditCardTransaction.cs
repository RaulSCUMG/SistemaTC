using static SistemaTC.Core.Enums;

namespace SistemaTC.Data.Entities;
public class CreditCardTransaction: Auditable
{
    public Guid CreditCardTransactionId { get; set; }
    public Guid UserId { get; set; }
    public Guid CreditCardId { get; set; }
    public Guid? CreditCutOffId { get; set; }
    public CreditCardTransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public User Owner { get; set; } = new();
    public CreditCard CreditCard { get; set; } = new();
    public CreditCutOff? CreditCutOff { get; set; }
}
