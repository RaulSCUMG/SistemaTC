namespace SistemaTC.Data.Entities;
public class Permission : Auditable
{
    public Guid PermissionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool Active { get; set; }

    public ICollection<RolePermission> Roles { get; set; } = new HashSet<RolePermission>();
}
