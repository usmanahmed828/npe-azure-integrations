using NPE.Core.Common.Enums;
using NPE.Core.Common.Settings;
using NPE.Core.Common.Tenancy.Services;

namespace NPE.Infrastructure.Common.Tenancy.Services
{
    public class TenantOwnershipResolver : ITenantOwnershipResolver
    {
        private readonly IDeploymentSettingsService _deploymentSettings;

        public TenantOwnershipResolver(IDeploymentSettingsService deploymentSettings)
        {
            _deploymentSettings = deploymentSettings;
        }

        public bool IsHybridLegacy()
        {
            return _deploymentSettings.GetDeploymentMode() == DeploymentMode.HybridLegacy;
        }

        public bool IsPureSaaS()
        {
            return _deploymentSettings.GetDeploymentMode() == DeploymentMode.PureSaaS;
        }
    }
}