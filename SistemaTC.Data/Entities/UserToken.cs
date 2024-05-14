namespace SistemaTC.Data.Entities;
public class UserToken : Auditable
{
    public Guid UserTokenId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set;} = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public bool Active { get; set; }
    public User User { get; set; } = default!;
}
