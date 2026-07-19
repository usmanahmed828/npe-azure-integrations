using NPE.Core.Common.Enums;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Settings;
using NPE.Core.Modules.Management.Centers.Services;
using NPE.Core.Modules.Management.Consultant.BusinessObjects;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.BusinessObjects;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Core.Modules.Signup.BusinessObjects;
using NPE.Core.Modules.Signup.Models;
using NPE.Core.Modules.Users.BusinessObjects;
using NPE.Core.Modules.Users.DTOs;
using NPE.Infrastructure.Modules.Tenancy;

namespace NPE.Infrastructure.Modules.Signup.Services
{
    public class SignupService : ISignupService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICenterService _centerService;
        private readonly IReferenceService _referenceService;
        private readonly IConsultantService _consultantService;
        private readonly IUserService _userService;
        private readonly ITenantProvisioningService _tenantProvisioningService;
        private readonly IDeploymentSettingsService _deploymentSettings;

        public SignupService(ApplicationDbContext context, IIdentityService identityService, ICenterService centerService,
            IReferenceService referenceService, IConsultantService consultantService, IUserService userService,
            ITenantProvisioningService tenantProvisioningService, IDeploymentSettingsService deploymentSettings)
        {
            _context = context;
            _identityService = identityService;
            _centerService = centerService;
            _referenceService = referenceService;
            _consultantService = consultantService;
            _userService = userService;
            _tenantProvisioningService = tenantProvisioningService;
            _deploymentSettings = deploymentSettings;
        }

        public async Task<SignupResponseDTO> SignupAsync(CompanySignupRequest request)
        {
            ValidateDeploymentMode();

            if (request.Center == null)
            {
                throw new Exception("Center information is required.");
            }

            #region Create Company

            var companyId = await _identityService.ConsumeIlockAsync(IdentityTypes.Company);

            var company = new iLock.Entities.ILockCompany
            {
                CompanyId = companyId,
                Name = request.CompanyName,
                Description = request.CompanyName
            };

            _context.ILockCompanies.Add(company);

            #endregion

            #region Create Root Center

            request.Center.CompanyId = companyId;
            request.Center.Name = request.CompanyName;
            request.Center.Address = request.Address;
            request.Center.Email = request.Email;
            request.Center.Phone = request.Phone;
            request.Center.City = request.CityId;
            request.Center.Country = request.CountryId;

            var centerId = await _centerService.CreateAsync(request.Center);

            #endregion

            #region Create Tenant Root Mapping
            await _centerService.AssignCenterToCompanyAsync(companyId, centerId, true);
            //var companyCenter = new Tenancy.Entities.CompanyCenter
            //{
            //    CompanyId = companyId,
            //    CenterId = centerId,
            //    IsRootCenter = true
            //};

            //_context.CompanyCenters.Add(companyCenter);

            #endregion

            #region Create Default Reference

            var referenceRequest = new ReferenceDTO
            {
                City = request.CityId,
                Country = request.CountryId
            };

            var referenceId = await _referenceService.CreateAsync(referenceRequest);

            await _centerService.SaveCenterReference(centerId, referenceId);

            //var companyReference = new CompanyReference
            //{
            //    CompanyId = companyId,
            //    ReferenceId = referenceId
            //};

            //_context.CompanyReferences.Add(companyReference);

            await _referenceService.AssignReferenceToCompanyAsync(companyId, referenceId);

            #endregion

            #region Create Default Consultant

            var consultantRequest = new ConsultantDto
            {
                City = request.CityId,
                Country = request.CountryId
            };

            var consultantId = await _consultantService.CreateAsync(consultantRequest);

            await _centerService.SaveCenterConsultant(centerId, consultantId);

            //var companyConsultant = new CompanyConsultant
            //{
            //    CompanyId = companyId,
            //    ConsultantId = consultantId
            //};

            //_context.CompanyConsultants.Add(companyConsultant);
            await _consultantService.AssignConsultantToCompanyAsync(companyId, consultantId);

            #endregion

            #region Create Additional Data

            await _centerService.SaveCenterAdditionalData(centerId, consultantId, referenceId);

            #endregion

            await _tenantProvisioningService.ProvisionAsync(companyId);

            var userId = await _userService.CreateAdminUserAsync(new CreateAdminUserRequest
            {
                CompanyId = companyId,
                RootCenterId = centerId,
                DefaultReferenceId = referenceId,
                Username = request.Username,
                Password = request.Password,
                FullName = request.ContactPerson,
                Email = request.Email,
                Phone = request.Phone
            });

            return new SignupResponseDTO
            {
                CompanyId = companyId,
                CenterId = centerId,
                UserId = userId,
                Message = "Company provisioned successfully"
            };
        }

        #region Helpers
        private void ValidateDeploymentMode()
        {
            if (_deploymentSettings.GetDeploymentMode() != DeploymentMode.PureSaaS)
            {
                throw new InvalidOperationException("Company signup is only available in PureSaaS mode.");
            }
        }

        #endregion
    }
}