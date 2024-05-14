namespace SistemaTC.Data.Entities;
public class Role : Auditable
{
    public Guid RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool Active { get; set; }

    public ICollection<Permission> Permissions { get; set;} = new HashSet<Permission>();
    public ICollection<User> Users { get; set; } = new HashSet<User>();
}
