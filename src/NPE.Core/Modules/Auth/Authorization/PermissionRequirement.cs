using Microsoft.AspNetCore.Authorization;

namespace NPE.Core.Modules.Auth.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public const string PolicyPrefix = "Permission:";

        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}