
using SistemaTC.Core;
using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.User;
public class ExistingUser: NewUser
{
    [Required]
    public Guid UserId { get; set; }
    [StringLength(General.User.PasswordLength)]
    [DataType(DataType.Password)]
    public new string? Password { get; set; }
}
