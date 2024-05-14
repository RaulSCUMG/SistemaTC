using SistemaTC.Core;
using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.User;
public class NewUser
{
    [Required]
    public Guid RoleId { get; set; }
    [Required]
    [StringLength(General.User.UserNameLength)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(General.User.EmailLength)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [StringLength(General.User.PasswordLength)]
    public string Password { get; set; } = string.Empty;
    [StringLength(General.User.FirstNameLength)]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(General.User.LastNameLength)]
    public string LastName { get; set; } = string.Empty;
    [StringLength(General.User.PhoneLength)]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string User { get; set; } = string.Empty;
}
