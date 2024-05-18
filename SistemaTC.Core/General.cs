namespace SistemaTC.Core;
public static class General
{
    public const string SystemUser = "System";

    public const int MoneyPrecision = 18;
    public const int MoneyScale = 2;

    public static class Roles
    {
        public const string Administrator = "admin";
        public const string Client = "client";
    }

    public static readonly Dictionary<string, Guid> RolesList = new() {
        { Roles.Administrator, Guid.Parse("e9f77193-3d01-4a99-aa01-6fc777d5a87a") },
        { Roles.Client, Guid.Parse("47fc7698-01d7-463a-a3c7-5c405fc4b562") }
    };

    public static class Permissions
    {
        public const string VIEW_ROLE = "VIEW_ROLE";

        public const string VIEW_USER = "VIEW_USER";
        public const string CREATE_USER = "CREATE_USER";
        public const string UPDATE_USER = "UPDATE_USER";
        public const string INACTIVATE_USER = "INACTIVATE_USER";

        public const string VIEW_CREDIT_CARD = "VIEW_CREDIT_CARD";
        public const string CREATE_CREDIT_CARD = "CREATE_CREDIT_CARD";
        public const string UPDATE_CREDIT_CARD = "UPDATE_CREDIT_CARD";
        public const string INACTIVATE_CREDIT_CARD = "INACTIVATE_CREDIT_CARD";
        public const string UPDATE_PIN_CREDIT_CARD = "UPDATE_PIN_CREDIT_CARD";

        public const string VIEW_REQUEST = "VIEW_REQUEST";
        public const string CREATE_REQUEST = "CREATE_REQUEST";
        public const string UPDATE_REQUEST = "UPDATE_REQUEST";
        public const string PROCESS_REQUEST = "PROCESS_REQUEST";

        public const string CREATE_CC_TRANSACTION = "CREATE_CC_TRANSACTION";

        public const string VIEW_CREDIT_CARD_CUTOFF = "VIEW_CREDIT_CARD_CUTOFF";
        public const string CREATE_CREDIT_CARD_CUTOFF = "CREATE_CREDIT_CARD_CUTOFF";
        public const string UPDATE_CREDIT_CARD_CUTOFF = "UPDATE_CREDIT_CARD_CUTOFF";

        public const string VIEW_CREDIT_CARD_PAYMENT = "VIEW_CREDIT_CARD_PAYMENT";
        public const string CREATE_CREDIT_CARD_PAYMENT = "CREATE_CREDIT_CARD_PAYMENT";

        public const string VIEW_CREDIT_CARD_INFO_FROM_USER = "VIEW_CREDIT_CARD_INFO_FROM_USER";

        public const string SEND_NOTIFICATION = "SEND_NOTIFICATION";
    }

    public static readonly Dictionary<string, Guid> PermissionsList = new() {
        { Permissions.VIEW_ROLE, Guid.Parse("389df558-751c-40dc-9cbf-9e847eac886d") },

        { Permissions.VIEW_USER, Guid.Parse("56c83f0f-53e0-4328-9ea7-9787bbcbfd02") },
        { Permissions.CREATE_USER, Guid.Parse("ef127738-9162-4654-89fc-d201386397f0") },
        { Permissions.UPDATE_USER, Guid.Parse("f8072b82-b1b1-4f5c-9eae-f2e7d1bd2634") },
        { Permissions.INACTIVATE_USER, Guid.Parse("7280baca-a1b4-42c9-9de2-b837849606a1") },

        { Permissions.VIEW_CREDIT_CARD, Guid.Parse("c3dd326f-3d61-4a36-934b-2dba1e256b8c") },
        { Permissions.CREATE_CREDIT_CARD, Guid.Parse("3d1ea76a-6607-456b-a584-142b32200f74") },
        { Permissions.UPDATE_CREDIT_CARD, Guid.Parse("b578352c-43d2-49ee-bbd0-c03d6f57e666") },
        { Permissions.INACTIVATE_CREDIT_CARD, Guid.Parse("4dadda0a-30a8-4fe7-83a2-936d9bf236b2") },
        { Permissions.UPDATE_PIN_CREDIT_CARD, Guid.Parse("ea36832a-eaca-40e6-afbd-dbc9c965a54f") },

        { Permissions.VIEW_REQUEST, Guid.Parse("002622c7-4619-4679-b084-fb16f892f557") },
        { Permissions.CREATE_REQUEST, Guid.Parse("abbfb829-aebf-4548-a89d-97570d11c04c") },
        { Permissions.UPDATE_REQUEST, Guid.Parse("d72db557-1203-48c2-866b-ba41d2f50447") },
        { Permissions.PROCESS_REQUEST, Guid.Parse("eda6a773-8c6b-48ec-aa9c-f81be8b46ea2") },

        { Permissions.CREATE_CC_TRANSACTION, Guid.Parse("33f7e6e5-458c-4f20-84ad-8407570fd568") },

        { Permissions.VIEW_CREDIT_CARD_CUTOFF, Guid.Parse("a38e0d18-6814-4e71-aa55-f66307bcd93b") },
        { Permissions.CREATE_CREDIT_CARD_CUTOFF, Guid.Parse("b8c1ee6e-393d-47e1-a58d-5bdb35e69b13") },
        { Permissions.UPDATE_CREDIT_CARD_CUTOFF, Guid.Parse("d21347ca-1c8b-49b6-8694-829801012975") },

        { Permissions.VIEW_CREDIT_CARD_PAYMENT, Guid.Parse("54978218-8f88-49e3-93fa-f7b4c2dd2c96") },
        { Permissions.CREATE_CREDIT_CARD_PAYMENT, Guid.Parse("c89fe7b5-bb41-44fd-a726-377095491585") },

        { Permissions.VIEW_CREDIT_CARD_INFO_FROM_USER, Guid.Parse("16d64a71-9d53-4672-bd88-c9dbfd237172") },

        { Permissions.SEND_NOTIFICATION, Guid.Parse("cf608b86-f8eb-4e75-b7c7-ecd6bde0da2b") }
    };


    public static class Permission
    {
        public const int NameLength = 100;
        public const int CodeLength = 50;
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
        public static readonly string NameTemplate = "Corte de Caja {date}";
        public static readonly string ChargeRateTemplate = "Cargo intereses";
    }
}
