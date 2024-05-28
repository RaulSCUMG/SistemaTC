using System.ComponentModel.DataAnnotations;
using static SistemaTC.Core.Enums;

namespace SistemaTC.DTO.Payment;
public class PaymentNew
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CreditCardId { get; set; }
    [Required]
    public Guid CreditCutOffId { get; set; }
    [Required]
    public decimal Amount { get; set; }
}
