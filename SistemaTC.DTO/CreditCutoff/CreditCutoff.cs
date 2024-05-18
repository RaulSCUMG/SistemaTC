namespace SistemaTC.DTO.CreditCutoff;
public class CreditCutoff
{
    public Guid CreditCutOffId { get; set; }
    public Guid UserId { get; set; }
    public Guid CreditCardId { get; set; }
    public string Name { get; set; } = default!;
    public ushort Year { get; set; }
    public ushort Month { get; set; }
    public decimal TotalCredit { get; set; }
    public decimal TotalDebit { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal PayedAmount { get; set; }
    public bool Payed { get; set; }
    public bool Closed { get; set; }

    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
}
