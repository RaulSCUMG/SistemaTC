using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.Role;
public class ExistingRole: NewRole
{
    [Required]
    public Guid RoleId { get; set; }
    [Required]
    public bool Active { get; set; }
}
