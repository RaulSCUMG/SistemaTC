
using System.ComponentModel.DataAnnotations;
using static SistemaTC.Core.Enums;

namespace SistemaTC.DTO.CreditCardTransaction;

public class CreditCardTransactionNew
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CreditCardId { get; set; }
    public Guid? CreditCutOffId { get; set; }
    [Required]
    public CreditCardTransactionType Type { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal Amount { get; set; }
}
