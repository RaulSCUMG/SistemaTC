namespace SistemaTC.Data.Entities;
public class User: Auditable
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool Active { get; set; }

    public Role Role { get; set; } = new();
    public ICollection<CreditCard> CreditCards { get; set; } = new HashSet<CreditCard>();
    public ICollection<CreditCardTransaction> CreditCardTransactions { get; set; } = new HashSet<CreditCardTransaction>();
    public ICollection<CreditCutOff> CreditCutOffs { get; set; } = new HashSet<CreditCutOff>();
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    public ICollection<Request> Requests { get; set; } = new HashSet<Request>();
    public ICollection<Request> RequestsAssigned { get; set; } = new HashSet<Request>();
    public ICollection<UserToken> Tokens { get; set; } = new HashSet<UserToken>();
}
