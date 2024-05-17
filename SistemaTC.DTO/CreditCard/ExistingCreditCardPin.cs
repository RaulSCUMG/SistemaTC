using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.CreditCard;

public class ExistingCreditCardPin : CreditCardNew
{
    [Required]
    public Guid CreditCardId { get; set; }
    [Required]
    public string Pin { get; set; } = string.Empty;
}
