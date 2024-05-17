using Microsoft.AspNetCore.Identity;

namespace SistemaTC.DTO.User;
public class LoggedInUser : IdentityUser
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
