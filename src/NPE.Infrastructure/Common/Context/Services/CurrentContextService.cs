using Microsoft.AspNetCore.Http;
using NPE.Core.Common.Context.DTOs;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Enums;
using NPE.Core.Common.Security;
using NPE.Core.Common.Settings;
using NPE.Core.Modules.Management.Centers.Services;
using System.Security.Claims;

namespace NPE.Infrastructure.Common.Context.Services
{
    public class CurrentContextService : ICurrentContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ICenterHierarchyService _centerHierarchyService;

        private readonly IDeploymentSettingsService _deploymentSettingsService;

        public CurrentContextService(IHttpContextAccessor httpContextAccessor, ICenterHierarchyService centerHierarchyService, IDeploymentSettingsService deploymentSettingsService)
        {
            _httpContextAccessor = httpContextAccessor;

            _centerHierarchyService = centerHierarchyService;

            _deploymentSettingsService = deploymentSettingsService;
        }

        public async Task<CurrentContextDTO>
            GetAsync()
        {
            var user =
                _httpContextAccessor
                    .HttpContext?
                    .User;

            if (user == null)
            {
                throw new InvalidOperationException(
                    "Current user context not found.");
            }

            var companyId =
    GetIntClaim(
        user,
        ClaimTypesCustom.CompanyId);

            var centerId =
    GetIntClaim(
        user,
        ClaimTypesCustom.CenterId);

            //var userId =
            //    GetIntClaim(
            //        user,
            //        ClaimTypes.NameIdentifier);
            var userId =
    GetIntClaim(
        user,
        ClaimTypesCustom.UserId);

            var userName =
                user.FindFirst(
                    ClaimTypes.Name)?.Value
                ?? "";

            var rootCenterId =
                await _centerHierarchyService
                    .GetRootCenterIdAsync(
                        centerId);

            return new CurrentContextDTO
            {
                CompanyId =
                    companyId,

                CenterId =
                    centerId,

                RootCenterId =
                    rootCenterId,

                UserId =
                    userId,

                UserName =
                    userName,

                IsPureSaaS =
                    _deploymentSettingsService
                        .GetDeploymentMode()
                    ==
                    DeploymentMode.PureSaaS
            };
        }

        #region Helpers

        private static int
            GetIntClaim(
                ClaimsPrincipal user,
                string claimType)
        {
            var value =
                user.FindFirst(
                    claimType)?.Value;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    $"Missing claim: {claimType}");
            }

            return int.Parse(value);
        }

        #endregion
    }
}