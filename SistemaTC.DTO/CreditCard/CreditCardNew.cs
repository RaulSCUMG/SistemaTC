using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.CreditCard;

public class CreditCardNew
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public decimal CreditLimit { get; set; }
    [Required]
    public decimal ChargeRate { get; set; }
}
