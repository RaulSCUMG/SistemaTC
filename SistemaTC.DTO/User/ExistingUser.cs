
using System.ComponentModel.DataAnnotations;

namespace SistemaTC.DTO.User;
public class ExistingUser: NewUser
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public bool Active { get; set; }
}
