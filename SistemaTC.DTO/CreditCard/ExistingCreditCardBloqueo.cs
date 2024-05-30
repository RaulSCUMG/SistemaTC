using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.CreditCard;

public class ExistingCreditCardBloqueo
{
    [Required]
    public Guid CreditCardId { get; set; }
    [Required]
    public bool Locked { get; set; }
}
