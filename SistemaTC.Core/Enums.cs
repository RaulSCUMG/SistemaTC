using System.ComponentModel;

namespace SistemaTC.Core;
public static class Enums
{
    public enum RequestType
    {
        [Description("Nueva Tarjeta de Crédito")]
        NewCreditCard,
        [Description("Aumento de Límite en Tarjeta de Crédito")]
        IncreaseCreditCardLimit
    }

    public enum CreditCardTransactionType
    {
        [Description("Débito")]
        Debit,
        [Description("Crédito")]
        Credit
    }

    public enum PaymentType
    {
        [Description("Contado")]
        Cash,
        [Description("Partial")]
        Partial
    }
}
