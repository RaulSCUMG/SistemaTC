using Microsoft.EntityFrameworkCore;
using static SistemaTC.Core.General;

namespace SistemaTC.Data;
public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Permission>(entity =>
        {
            entity.HasData(

            #region Role
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_ROLE],
                        Code = Permissions.VIEW_ROLE,
                        Name = "View Role",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 17, 3, 23, 0)
                    }
                    #endregion Role
                    ,

            #region User
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_USER],
                        Code = Permissions.VIEW_USER,
                        Name = "View User",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.CREATE_USER],
                        Code = Permissions.CREATE_USER,
                        Name = "Create User",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.UPDATE_USER],
                        Code = Permissions.UPDATE_USER,
                        Name = "Update User",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.INACTIVATE_USER],
                        Code = Permissions.INACTIVATE_USER,
                        Name = "Inactivate/Activate User",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion User
                    ,

            #region Credit Card
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD],
                        Code = Permissions.VIEW_CREDIT_CARD,
                        Name = "View Credit Card",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.CREATE_CREDIT_CARD],
                        Code = Permissions.CREATE_CREDIT_CARD,
                        Name = "Create Credit Card",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.UPDATE_CREDIT_CARD],
                        Code = Permissions.UPDATE_CREDIT_CARD,
                        Name = "Update Credit Card",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.INACTIVATE_CREDIT_CARD],
                        Code = Permissions.INACTIVATE_CREDIT_CARD,
                        Name = "Inactivate/Activate Credit Card",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.UPDATE_PIN_CREDIT_CARD],
                        Code = Permissions.UPDATE_PIN_CREDIT_CARD,
                        Name = "Update PIN of Credit Card",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion Credit Card
                    ,

            #region Request
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_REQUEST],
                        Code = Permissions.VIEW_REQUEST,
                        Name = "View Request",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.CREATE_REQUEST],
                        Code = Permissions.CREATE_REQUEST,
                        Name = "Create Request",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.UPDATE_REQUEST],
                        Code = Permissions.UPDATE_REQUEST,
                        Name = "Update Request",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.PROCESS_REQUEST],
                        Code = Permissions.PROCESS_REQUEST,
                        Name = "Process Request",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion Request
                    ,

            #region Credit Card Transaction
                    new
                    {
                        PermissionId = PermissionsList[Permissions.CREATE_CC_TRANSACTION],
                        Code = Permissions.CREATE_CC_TRANSACTION,
                        Name = "Create Credit/Debit Credit Card Transaction",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion Credit Card Transaction
                    ,

            #region Credit Card Cutoff
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_CUTOFF],
                        Code = Permissions.VIEW_CREDIT_CARD_CUTOFF,
                        Name = "View Credit Card Cutoff",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.CREATE_CREDIT_CARD_CUTOFF],
                        Code = Permissions.CREATE_CREDIT_CARD_CUTOFF,
                        Name = "Create Credit Card Cutoff",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.UPDATE_CREDIT_CARD_CUTOFF],
                        Code = Permissions.UPDATE_CREDIT_CARD_CUTOFF,
                        Name = "Update Credit Card Cutoff",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion Credit Card Cutoff
                    ,

            #region Credit Card Cutoff
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_PAYMENT],
                        Code = Permissions.VIEW_CREDIT_CARD_PAYMENT,
                        Name = "View Credit Card Payment",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    },
                    new
                    {
                        PermissionId = PermissionsList[Permissions.CREATE_CREDIT_CARD_PAYMENT],
                        Code = Permissions.CREATE_CREDIT_CARD_PAYMENT,
                        Name = "Create Credit Card Payment",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion Credit Card Cutoff
                    ,

            #region View Credit Card Info From Other User
                    new
                    {
                        PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_INFO_FROM_USER],
                        Code = Permissions.VIEW_CREDIT_CARD_INFO_FROM_USER,
                        Name = "View Credit Card Info from other user",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 14, 1, 24, 0)
                    }
                    #endregion Credit Card Cutoff
                    ,

            #region Send Notification
                    new
                    {
                        PermissionId = PermissionsList[Permissions.SEND_NOTIFICATION],
                        Code = Permissions.SEND_NOTIFICATION,
                        Name = "Send Notification",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 17, 3, 23, 0)
                    }
                    #endregion Send Notification
                );
        });

        modelBuilder.Entity<Entities.Role>(entity =>
        {
            entity.HasData(
                    new
                    {
                        RoleId = RolesList[Roles.Administrator],
                        Code = Roles.Administrator,
                        Name = "Administrador",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 11, 0, 36, 0)
                    },
                    new
                    {
                        RoleId = RolesList[Roles.Client],
                        Code = Roles.Client,
                        Name = "Cliente",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 17, 3, 23, 0)
                    }
                );
        });

        modelBuilder.Entity<Entities.RolePermission>(entity =>
        {
            entity.HasData(
            #region Administrator
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_ROLE] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_USER] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.CREATE_USER] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.UPDATE_USER] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.INACTIVATE_USER] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.CREATE_CREDIT_CARD] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.UPDATE_CREDIT_CARD] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.INACTIVATE_CREDIT_CARD] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.UPDATE_PIN_CREDIT_CARD] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_REQUEST] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.CREATE_REQUEST] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.UPDATE_REQUEST] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.PROCESS_REQUEST] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.CREATE_CC_TRANSACTION] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_CUTOFF] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.CREATE_CREDIT_CARD_CUTOFF] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.UPDATE_CREDIT_CARD_CUTOFF] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_PAYMENT] },
                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.CREATE_CREDIT_CARD_PAYMENT] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_INFO_FROM_USER] },

                     new() { RoleId = RolesList[Roles.Administrator], PermissionId = PermissionsList[Permissions.SEND_NOTIFICATION] },
                     #endregion Administrator

                     #region Client
                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.VIEW_USER] },
                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.UPDATE_USER] },

                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD] },
                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.INACTIVATE_CREDIT_CARD] },
                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.UPDATE_PIN_CREDIT_CARD] },

                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.VIEW_REQUEST] },
                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.CREATE_REQUEST] },

                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_CUTOFF] },

                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.VIEW_CREDIT_CARD_PAYMENT] },

                     new() { RoleId = RolesList[Roles.Client], PermissionId = PermissionsList[Permissions.SEND_NOTIFICATION] }
                     #endregion Client
                );
        });
    }
}
