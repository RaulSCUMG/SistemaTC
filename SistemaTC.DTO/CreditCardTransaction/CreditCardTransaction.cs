namespace SistemaTC.DTO.CreditCardTransaction;
public class CreditCardTransaction : CreditCardTransactionNew
{
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}