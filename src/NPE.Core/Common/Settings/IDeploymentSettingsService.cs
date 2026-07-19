using NPE.Core.Common.Enums;

namespace NPE.Core.Common.Settings
{
    public interface IDeploymentSettingsService
    {
        DeploymentMode
            GetDeploymentMode();
    }
}
