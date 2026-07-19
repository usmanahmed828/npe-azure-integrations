using Microsoft.AspNetCore.Authorization;
using NPE.Core.Modules.Auth.Authorization;

namespace NPE.Infrastructure.Modules.Auth.Authorization
{
    public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            // External app permission
            var hasPermission = context.User.Claims.Any(c =>
                c.Type == "permission" &&
                c.Value == requirement.Permission);

            if (hasPermission)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
