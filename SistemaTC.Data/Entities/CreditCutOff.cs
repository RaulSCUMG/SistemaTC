namespace SistemaTC.Data.Entities;

public class CreditCutOff: Auditable
{
    public Guid CreditCutOffId { get; set; }
    public Guid UserId { get; set; }
    public Guid CreditCardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ushort Year { get; set; }
    public ushort Month { get; set; }
    public decimal TotalCredit { get; set; }
    public decimal TotalDebit { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal TotalCreditAvailable { get; set; }
    public decimal PayedAmount { get; set; } = 0M;
    public bool Payed { get; set; }
    public bool Closed { get; set; }

    public User Owner { get; set; } = default!;
    public CreditCard CreditCard { get; set; } = default!;
    public ICollection<CreditCardTransaction> CreditCardTransactions { get; set; } = new HashSet<CreditCardTransaction>();
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
}