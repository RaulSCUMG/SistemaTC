using SistemaTC.Core;
using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.Role;
public class NewRole
{
    [Required]
    [StringLength(General.Role.NameLength)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(General.Role.CodeLength)]
    public string Code { get; set; } = string.Empty;
    [Required]
    public string User { get; set; } = string.Empty;
}
