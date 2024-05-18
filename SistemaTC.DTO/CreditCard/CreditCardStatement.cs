namespace SistemaTC.DTO.CreditCard;
public class CreditCardStatement
{
    public Guid CreditCardId { get; set; }
    public string OwnerName { get; set; } = default!;
    public DateTime NextBalanceCutOffDate { get; set; }
    public DateTime NextPaymentDate { get; set; }
    public decimal CreditAvailable { get; set; }
    public decimal Balance { get; set; }
    public List<CreditCardStatementDetail> Detail { get; set; } = [];
}

public class CreditCardStatementDetail
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}
