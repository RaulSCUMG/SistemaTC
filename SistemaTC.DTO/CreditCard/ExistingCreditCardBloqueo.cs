using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.CreditCard;

public class ExistingCreditCardBloqueo : CreditCardNew
{
    [Required]
    public Guid CreditCardId { get; set; }
    [Required]
    public bool Locked { get; set; }
}
