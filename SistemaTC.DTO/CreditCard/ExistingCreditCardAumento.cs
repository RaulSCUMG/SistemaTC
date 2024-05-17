using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.CreditCard;

public class ExistingCreditCardAumento : CreditCardNew
{
    [Required]
    public Guid CreditCardId { get; set; }
    [Required]
    public decimal CreditLimit { get; set; } = 0;
}
