using SistemaTC.DTO.User;
using System.Security.Claims;

namespace SistemaTC.Api.Middleware;

public class AuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated)
        {
            var loggedInUser = new LoggedInUser { 
                UserId = Guid.Parse(context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value!),
                UserName = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                Email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                RoleId = Guid.Parse(context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!)
            };

            context.Items[nameof(LoggedInUser)] = loggedInUser;
        }

        await next(context);
    }
}
public static class AuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationMiddleware>();
    }
}