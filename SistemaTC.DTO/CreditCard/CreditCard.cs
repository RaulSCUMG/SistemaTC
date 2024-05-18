﻿namespace SistemaTC.DTO.CreditCard;

public class CreditCard : ExistingCreditCardPin
{
    public DateTime NextBalanceCutOffDate { get; set; }
    public DateTime NextPaymentDate { get; set; }
    public decimal CreditAvailable { get; set; }
    public decimal ChargeRate { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}
