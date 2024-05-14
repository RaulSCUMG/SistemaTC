using Microsoft.EntityFrameworkCore;
using SistemaTC.Core;
using SistemaTC.Data.Entities;
using SistemaTC.Data.Extensions;

namespace SistemaTC.Data;
public class TCContext(DbContextOptions options) : DbContext(options), ITCContext
{
    public virtual DbSet<CreditCard> CreditCards { get; set; }
    public virtual DbSet<CreditCardTransaction> CreditCardTransactions { get; set; }
    public virtual DbSet<CreditCutOff> CreditCutOffs { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RolePermission> RolePermissions { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.ToTable(nameof(CreditCard));
            entity.AddAuditableFields();
            entity.HasKey(x => x.CreditCardId);

            entity.Property(x => x.Number).IsRequired().HasMaxLength(General.CreditCard.NumberLength).IsUnicode();
            entity.Property(x => x.ExpirationDate).IsRequired();
            entity.Property(x => x.Expired).IsRequired();
            entity.Property(x => x.Pin).IsRequired().HasMaxLength(General.CreditCard.PinLength);
            entity.Property(x => x.Ccv).IsRequired().HasMaxLength(General.CreditCard.CcvLength);
            entity.Property(x => x.CreditLimit).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.CreditAvailable).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.ChargeRate).IsRequired().HasPrecision(General.CreditCard.ChargeRatePrecision, General.CreditCard.ChargeRateScale);
            entity.Property(x => x.BalanceCutOffDay).IsRequired();
            entity.Property(x => x.NextBalanceCutOffDate).IsRequired();
            entity.Property(x => x.PaymentDay).IsRequired();
            entity.Property(x => x.NextPaymentDate).IsRequired();
            entity.Property(x => x.Active).IsRequired().HasDefaultValue(false);
            entity.Property(x => x.ActivationDate);
            entity.Property(x => x.Locked).IsRequired().HasDefaultValue(false);
            entity.Property(x => x.LockedDate);
            entity.Property(x => x.UserId).IsRequired();

            entity.HasIndex(x => x.Number);
            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.Active);

            entity.HasMany(x => x.CreditCardTransactions).WithOne(x => x.CreditCard).HasForeignKey(x => x.CreditCardId);
            entity.HasMany(x => x.CreditCutOffs).WithOne(x => x.CreditCard).HasForeignKey(x => x.CreditCardId);
            entity.HasMany(x => x.Payments).WithOne(x => x.CreditCard).HasForeignKey(x => x.CreditCardId);
        });

        modelBuilder.Entity<CreditCardTransaction>(entity =>
        {
            entity.ToTable(nameof(CreditCardTransaction));
            entity.AddAuditableFields();
            entity.HasKey(x => x.CreditCardTransactionId);

            entity.Property(x => x.Type).IsRequired();
            entity.Property(x => x.Description).IsRequired().HasMaxLength(General.CreditCardTransaction.DescriptionLength).IsUnicode();
            entity.Property(x => x.Amount).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.CreditCardId).IsRequired();

            entity.HasIndex(x => x.Type);
            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.CreditCardId);
            entity.HasIndex(x => x.CreditCutOffId);
        });

        modelBuilder.Entity<CreditCutOff>(entity =>
        {
            entity.ToTable(nameof(CreditCutOff));
            entity.AddAuditableFields();
            entity.HasKey(x => x.CreditCutOffId);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(General.CreditCutOff.NameLength).IsUnicode();
            entity.Property(x => x.Year).IsRequired();
            entity.Property(x => x.Month).IsRequired();
            entity.Property(x => x.TotalCredit).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.TotalDebit).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.TotalBalance).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.PayedAmount).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.Payed).IsRequired().HasDefaultValue(false);
            entity.Property(x => x.Closed).IsRequired().HasDefaultValue(false);
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.CreditCardId).IsRequired();

            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.CreditCardId);

            entity.HasMany(x => x.CreditCardTransactions).WithOne(x => x.CreditCutOff).HasForeignKey(x => x.CreditCutOffId);
            entity.HasMany(x => x.Payments).WithOne(x => x.CreditCutOff).HasForeignKey(x => x.CreditCutOffId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable(nameof(Payment));
            entity.AddAuditableFields();
            entity.HasKey(x => x.PaymentId);

            entity.Property(x => x.Type).IsRequired();
            entity.Property(x => x.Amount).IsRequired().HasPrecision(General.MoneyPrecision, General.MoneyScale);
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.CreditCardId).IsRequired();
            entity.Property(x => x.CreditCutOffId).IsRequired();

            entity.HasIndex(x => x.Type);
            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.CreditCardId);
            entity.HasIndex(x => x.CreditCutOffId);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable(nameof(Permission));
            entity.AddAuditableFields();
            entity.HasKey(x => x.PermissionId);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(General.Permission.NameLength).IsUnicode();
            entity.Property(x => x.Code).IsRequired().HasMaxLength(General.Permission.CodeLength).IsUnicode();
            entity.Property(x => x.Active).IsRequired().HasDefaultValue(true);

            entity.HasIndex(x => x.Code);
            entity.HasIndex(x => x.Active);

            entity.HasMany(x => x.Roles).WithOne(x => x.Permission).HasForeignKey(x => x.PermissionId);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable(nameof(Request));
            entity.AddAuditableFields();
            entity.HasKey(x => x.RequestId);

            entity.Property(x => x.Number).IsRequired();
            entity.Property(x => x.Type).IsRequired();
            entity.Property(x => x.Note).IsRequired().IsUnicode();
            entity.Property(x => x.InternalNote).IsRequired().IsUnicode();
            entity.Property(x => x.Approved).IsRequired().HasDefaultValue(false);
            entity.Property(x => x.RequestedByUserId).IsRequired();
            entity.Property(x => x.AssignedToUserId);

            entity.HasIndex(x => x.Number);
            entity.HasIndex(x => x.Type);
            entity.HasIndex(x => x.RequestedByUserId);
            entity.HasIndex(x => x.AssignedToUserId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable(nameof(Role));
            entity.AddAuditableFields();
            entity.HasKey(x => x.RoleId);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(General.Role.NameLength).IsUnicode();
            entity.Property(x => x.Code).IsRequired().HasMaxLength(General.Role.CodeLength).IsUnicode();
            entity.Property(x => x.Active).IsRequired().HasDefaultValue(true);

            entity.HasIndex(x => x.Code);
            entity.HasIndex(x => x.Active);

            entity.HasMany(x => x.Users).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
            entity.HasMany(x => x.Permissions).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable(nameof(RolePermission));
            entity.HasKey(x => new { x.RoleId, x.PermissionId });

            entity.HasIndex(x => x.RoleId);
            entity.HasIndex(x => x.PermissionId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(nameof(User));
            entity.AddAuditableFields();
            entity.HasKey(x => x.UserId);

            entity.Property(x => x.UserName).IsRequired().HasMaxLength(General.User.UserNameLength).IsUnicode();
            entity.Property(x => x.Email).IsRequired().HasMaxLength(General.User.EmailLength).IsUnicode();
            entity.Property(x => x.Password).IsRequired().HasMaxLength(General.User.PasswordLength).IsUnicode();
            entity.Property(x => x.FirstName).IsRequired().HasMaxLength(General.User.FirstNameLength).IsUnicode();
            entity.Property(x => x.LastName).IsRequired().HasMaxLength(General.User.LastNameLength).IsUnicode();
            entity.Property(x => x.Phone).IsRequired().HasMaxLength(General.User.PhoneLength).IsUnicode();
            entity.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            entity.Property(x => x.RoleId).IsRequired();

            entity.HasIndex(x => x.UserName);
            entity.HasIndex(x => x.Email);
            entity.HasIndex(x => x.Active);

            entity.HasMany(x => x.CreditCards).WithOne(x => x.Owner).HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.CreditCardTransactions).WithOne(x => x.Owner).HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.CreditCutOffs).WithOne(x => x.Owner).HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.Payments).WithOne(x => x.Owner).HasForeignKey(x => x.UserId);
            entity.HasMany(x => x.Requests).WithOne(x => x.RequestedBy).HasForeignKey(x => x.RequestedByUserId);
            entity.HasMany(x => x.RequestsAssigned).WithOne(x => x.AssignedTo).HasForeignKey(x => x.AssignedToUserId);
            entity.HasMany(x => x.Tokens).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable(nameof(UserToken));
            entity.AddAuditableFields();
            entity.HasKey(x => x.UserTokenId);

            entity.Property(x => x.Token).IsRequired().IsUnicode();
            entity.Property(x => x.ExpirationDate).IsRequired();
            entity.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            entity.Property(x => x.UserId).IsRequired();

            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.Active);
        });

        modelBuilder.Seed();
    }
    public int? GetCommandTimeout() => Database.GetCommandTimeout();
    public void SetCommandTimeout(int? timeout) => Database.SetCommandTimeout(timeout);
}
