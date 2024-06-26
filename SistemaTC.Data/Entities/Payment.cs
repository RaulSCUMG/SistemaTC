﻿using static SistemaTC.Core.Enums;

namespace SistemaTC.Data.Entities;

public class Payment: Auditable
{
    public Guid PaymentId { get; set; }
    public Guid UserId { get; set; }
    public Guid CreditCardId { get; set; }
    public Guid CreditCutOffId { get; set; }
    public PaymentType Type { get; set; }
    public decimal Amount { get; set; }

    public User Owner { get; set; } = default!;
    public CreditCard CreditCard { get; set; } = default!;
    public CreditCutOff CreditCutOff { get; set; } = default!;
}