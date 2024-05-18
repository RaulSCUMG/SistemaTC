namespace SistemaTC.DTO.CreditCard;

public class CreditCardResponseFecha
{
    public Guid CreditCardId { get; set; }
    public DateTime PreviousBalanceCutOffDate { get; set; }
    public DateTime NextBalanceCutOffDate { get; set; }
}