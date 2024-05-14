using Microsoft.EntityFrameworkCore;
using SistemaTC.Data.Entities;

namespace SistemaTC.Data;
public interface ITCContext
{
    DbSet<CreditCard> CreditCards { get; set; }
    DbSet<CreditCardTransaction> CreditCardTransactions { get; set; }
    DbSet<CreditCutOff> CreditCutOffs { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<Request> Requests { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserToken> UserTokens { get; set; }

    public int? GetCommandTimeout();
    public void SetCommandTimeout(int? timeout);
}
