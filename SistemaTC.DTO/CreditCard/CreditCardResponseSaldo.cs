namespace SistemaTC.DTO.CreditCard;

public class CreditCardResponseSaldo
{
    public Guid CreditCardId { get; set; }
    public decimal CreditAvailable { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal BalanceAtCutOff { get; set; }
}
