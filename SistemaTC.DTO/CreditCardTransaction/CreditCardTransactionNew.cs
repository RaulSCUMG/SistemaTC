
using System.ComponentModel.DataAnnotations;
using static SistemaTC.Core.Enums;

namespace SistemaTC.DTO.CreditCardTransaction;

public class CreditCardTransactionNew
{
    public Guid UserId { get; set; }
    public Guid CreditCardId { get; set; }
    public Guid? CreditCutOffId { get; set; }
    public CreditCardTransactionType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }

}
