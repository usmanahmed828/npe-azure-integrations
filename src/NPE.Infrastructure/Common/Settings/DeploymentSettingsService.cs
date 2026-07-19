using Microsoft.Extensions.Configuration;

using NPE.Core.Common.Enums;
using NPE.Core.Common.Settings;

namespace NPE.Infrastructure.Common.Settings
{
    public class DeploymentSettingsService : IDeploymentSettingsService
    {
        private readonly IConfiguration _configuration;

        public DeploymentSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DeploymentMode GetDeploymentMode()
        {
            var value = _configuration["Deployment:Mode"];

            return Enum.TryParse<DeploymentMode>(value, true, out var mode) ? mode : DeploymentMode.HybridLegacy;
        }
    }
}