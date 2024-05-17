using Microsoft.AspNetCore.Authorization;

namespace SistemaTC.Api.Filters;

public class PermissionRequirement(IEnumerable<string> permissions) : IAuthorizationRequirement
{
    public IEnumerable<string> Permissions { get; } = permissions;
}
