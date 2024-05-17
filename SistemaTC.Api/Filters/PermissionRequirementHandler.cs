using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaTC.Data;
using System.Security.Claims;

namespace SistemaTC.Api.Filters;

public class PermissionRequirementHandler(ITCContext dbContext) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
        {
            return;
        }

        var roleId = Guid.Parse(context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? Guid.Empty.ToString());

        var userPermissions = await dbContext.RolePermissions.AsNoTracking()
            .Where(x => x.RoleId == roleId)
            .Select(x => x.Permission.Code)
            .ToListAsync();

        if (requirement.Permissions.All(rp => userPermissions.Contains(rp)))
        {
            context.Succeed(requirement);
        }
    }
}
