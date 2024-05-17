using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SistemaTC.Api.Filters;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    const string POLICY_PREFIX = "Over18";
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
        Task.FromResult(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() =>
        Task.FromResult<AuthorizationPolicy>(null!);
    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {

        if (policyName.StartsWith(nameof(PermissionRequirement), StringComparison.OrdinalIgnoreCase))
        {
            var permissions = policyName[(nameof(PermissionRequirement).Length + 1)..];
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            policy.AddRequirements(new PermissionRequirement(permissions.Split(',')));
            return Task.FromResult(policy.Build());
        }
        return Task.FromResult<AuthorizationPolicy>(null!);
    }
}
