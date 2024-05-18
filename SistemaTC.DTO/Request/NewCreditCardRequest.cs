using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.Request;
public class NewCreditCardRequest: UpdateRequest
{
    [Required]
    [DataType(DataType.Currency)]
    public decimal CreditLimit { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public decimal ChargeRate { get; set; }
}
