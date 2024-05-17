using Microsoft.AspNetCore.Authorization;

namespace SistemaTC.Api.Filters;

public class PermissionAuthorizationAttribute : AuthorizeAttribute
{
    public PermissionAuthorizationAttribute(params string[] permissions)
    {
        Policy = $"{nameof(PermissionRequirement)}:{string.Join(",", permissions)}";
    }
}
