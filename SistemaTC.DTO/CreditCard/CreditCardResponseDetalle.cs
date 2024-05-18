namespace SistemaTC.DTO.CreditCard;

public class CreditCardResponseDetalle
{
    public Guid CreditCardId { get; set; }
    public decimal CreditAvailable { get; set; }
    public DateOnly NextPaymentDate { get; set; }
    public decimal ChargeRate { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal BalanceAtCutOff { get; set; }
    public DateOnly NextBalanceCutOffDate { get; set; }
}
