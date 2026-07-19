using Microsoft.AspNetCore.Authorization;

namespace NPE.Core.Modules.Auth.Authorization
{
    public class RequiresPermissionAttribute : AuthorizeAttribute
    {
        public RequiresPermissionAttribute(string permission)
        {
            Policy = PermissionRequirement.PolicyPrefix + permission;
        }
    }
}
