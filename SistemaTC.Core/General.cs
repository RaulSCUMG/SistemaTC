namespace SistemaTC.Core;
public static class General
{
    public const string SystemUser = "System";

    public const int MoneyPrecision = 18;
    public const int MoneyScale = 2;

    public static class Roles
    {
        public const string Administrator = "admin";
    }

    public static readonly Dictionary<string, Guid> RolesList = new() {
        { Roles.Administrator, Guid.Parse("e9f77193-3d01-4a99-aa01-6fc777d5a87a") }
    };


    public static class Permission
    {
        public const int NameLength = 25;
        public const int CodeLength = 10;
    }

    public static class Role
    {
        public const int NameLength = 25;
        public const int CodeLength = 10;
    }

    public static class User
    {
        public const int UserNameLength = 100;
        public const int EmailLength = 250;
        public const int PasswordLength = 1000;
        public const int FirstNameLength = 250;
        public const int LastNameLength = 250;
        public const int PhoneLength = 15;
    }

    public static class CreditCard
    {
        public const int NumberLength = 19;
        public const int PinLength = 4;
        public const int CcvLength = 6;
        public const int ChargeRatePrecision = 5;
        public const int ChargeRateScale = 2;
    }

    public static class CreditCardTransaction
    {
        public const int DescriptionLength = 100;
    }

    public static class CreditCutOff
    {
        public const int NameLength = 100;
    }
}
