namespace SistemaTC.DTO.CreditCard;

public class CreditCardResponseFecha
{
    public Guid CreditCardId { get; set; }
    public DateOnly PreviousBalanceCutOffDate { get; set; }
    public DateOnly NextBalanceCutOffDate { get; set; }
}