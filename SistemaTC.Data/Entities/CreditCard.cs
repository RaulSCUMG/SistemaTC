namespace SistemaTC.Data.Entities;
public class CreditCard: Auditable
{
    public Guid CreditCardId { get; set; }
    public Guid UserId { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public bool Expired { get; set; }
    public string Pin { get; set; } = string.Empty;
    public string Ccv { get; set; } = string.Empty;
    public decimal CreditLimit { get; set; }
    public decimal CreditAvailable { get; set; }
    public decimal ChargeRate { get; set; }
    public int BalanceCutOffDay { get; set; }
    public DateTime NextBalanceCutOffDate { get; set; }
    public int PaymentDay { get; set; }
    public DateTime NextPaymentDate { get; set; }
    public bool Active { get; set; }
    public DateTime? ActivationDate { get; set; }
    public bool Locked { get; set; }
    public DateTime? LockedDate { get; set; }

    public User Owner { get; set; } = default!;
    public ICollection<CreditCardTransaction> CreditCardTransactions { get; set; } = new HashSet<CreditCardTransaction>();
    public ICollection<CreditCutOff> CreditCutOffs { get; set; } = new HashSet<CreditCutOff>();
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
}
