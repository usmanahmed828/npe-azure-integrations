using Microsoft.EntityFrameworkCore;

using NPE.Core.Common.Context.DTOs;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Enums;
using NPE.Core.Common.Policies;
using NPE.Core.Common.Settings;

using NPE.Core.Modules.Lookups.Services;

using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;

namespace NPE.Infrastructure.Modules.Lookups.Services
{
    public class LookupResolverService : ILookupResolverService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILookupPolicyService _policyService;
        private readonly IDeploymentSettingsService _deploymentSettings;
        private readonly ICurrentContextService _currentContextService;

        public LookupResolverService(ApplicationDbContext context, ILookupPolicyService policyService, IDeploymentSettingsService deploymentSettings, ICurrentContextService currentContextService)
        {
            _context = context;
            _policyService = policyService;
            _deploymentSettings = deploymentSettings;
            _currentContextService = currentContextService;
        }

        #region Public Methods

        public async Task<List<ConsultantLookupDTO>> GetConsultantsAsync()
        {
            var context = await _currentContextService.GetAsync();

            var policy = await _policyService.GetPolicyAsync(context.CompanyId);

            if (IsPureSaaS())
            {
                return await GetSaaSConsultantsAsync(context, policy);
            }

            return await GetLegacyConsultantsAsync(context, policy);
        }
        public async Task<List<ReferenceLookupDTO>> GetReferencesAsync()
        {
            var context = await _currentContextService.GetAsync();

            var policy = await _policyService.GetPolicyAsync(context.CompanyId);

            if (IsPureSaaS())
            {
                return await GetSaaSReferencesAsync(context, policy);
            }

            return await GetLegacyReferencesAsync(context, policy);
        }

        #endregion

        #region Deployment Routing

        private async Task<List<ConsultantLookupDTO>> GetLegacyConsultantsAsync(CurrentContextDTO context, LookupPolicyDTO policy)
        {
            return policy.ConsultantAccessMode switch
            {
                LookupAccessMode.Company => await GetLegacyCompanyConsultantsAsync(),

                LookupAccessMode.Center => await GetLegacyCenterConsultantsAsync(context.CenterId),

                LookupAccessMode.User => await GetLegacyUserConsultantsAsync(context.UserId),
                _ => new List<ConsultantLookupDTO>()
            };
        }

        private async Task<List<ConsultantLookupDTO>> GetSaaSConsultantsAsync(CurrentContextDTO context, LookupPolicyDTO policy)
        {
            return policy.ConsultantAccessMode switch
            {
                LookupAccessMode.Company => await GetSaaSCompanyConsultantsAsync(context.CompanyId),

                LookupAccessMode.Center => await GetSaaSCenterConsultantsAsync(context.CompanyId, context.CenterId),

                LookupAccessMode.User => await GetSaaSUserConsultantsAsync(context.UserId),
                _ => new List<ConsultantLookupDTO>()
            };
        }

        private async Task<List<ReferenceLookupDTO>> GetLegacyReferencesAsync(CurrentContextDTO context, LookupPolicyDTO policy)
        {
            return policy.ReferenceAccessMode switch
            {
                LookupAccessMode.Company => await GetLegacyCompanyReferencesAsync(),

                LookupAccessMode.Center => await GetLegacyCenterReferencesAsync(context.CenterId),

                LookupAccessMode.User => await GetLegacyUserReferencesAsync(context.UserId),
                _ => new List<ReferenceLookupDTO>()
            };
        }

        private async Task<List<ReferenceLookupDTO>> GetSaaSReferencesAsync(CurrentContextDTO context, LookupPolicyDTO policy)
        {
            return policy.ReferenceAccessMode switch
            {
                LookupAccessMode.Company => await GetSaaSCompanyReferencesAsync(context.CompanyId),

                LookupAccessMode.Center => await GetSaaSCenterReferencesAsync(context.CompanyId, context.CenterId),

                LookupAccessMode.User => await GetSaaSUserReferencesAsync(context.UserId),
                _ => new List<ReferenceLookupDTO>()
            };
        }

        #endregion

        #region Legacy Consultants

        private async Task<List<ConsultantLookupDTO>> GetLegacyCompanyConsultantsAsync()
        {
            return await _context.Consultants
                .AsNoTracking()
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => MapConsultant(x))
                .ToListAsync();
        }

        private async Task<List<ConsultantLookupDTO>> GetLegacyCenterConsultantsAsync(int centerId)
        {
            return await _context.CenterConsultants
                .AsNoTracking()
                .Where(x => x.CenterId == centerId)
                .Select(x => x.Consultant)
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => MapConsultant(x))
                .Distinct()
                .ToListAsync();
        }

        private async Task<List<ConsultantLookupDTO>> GetLegacyUserConsultantsAsync(int userId)
        {
            await Task.CompletedTask;

            return new List<ConsultantLookupDTO>();
        }

        #endregion

        #region SaaS Consultants

        private async Task<List<ConsultantLookupDTO>> GetSaaSCompanyConsultantsAsync(int companyId)
        {
            return await _context.CompanyConsultants
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.Consultant)
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => MapConsultant(x))
                .Distinct()
                .ToListAsync();
        }

        private async Task<List<ConsultantLookupDTO>> GetSaaSCenterConsultantsAsync(int companyId, int centerId)
        {
            return await _context.CompanyConsultants
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.Consultant)
                .Where(x => x.Status)
                .Where(x => x.CenterConsultants.Any(cc => cc.CenterId == centerId))
                .OrderBy(x => x.Name)
                .Select(x => MapConsultant(x))
                .Distinct()
                .ToListAsync();
        }

        private async Task<List<ConsultantLookupDTO>> GetSaaSUserConsultantsAsync(int userId)
        {
            await Task.CompletedTask;

            return new List<ConsultantLookupDTO>();
        }

        #endregion

        #region Legacy References

        private async Task<List<ReferenceLookupDTO>> GetLegacyCompanyReferencesAsync()
        {
            return await _context.References
                .AsNoTracking()
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => MapReference(x))
                .ToListAsync();
        }

        private async Task<List<ReferenceLookupDTO>> GetLegacyCenterReferencesAsync(int centerId)
        {
            return await _context.CenterReferences
                .AsNoTracking()
                .Where(x => x.CenterId == centerId)
                .Select(x => x.Reference)
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => MapReference(x))
                .Distinct()
                .ToListAsync();
        }

        private async Task<List<ReferenceLookupDTO>> GetLegacyUserReferencesAsync(int userId)
        {
            await Task.CompletedTask;

            return new List<ReferenceLookupDTO>();
        }

        #endregion

        #region SaaS References

        private async Task<List<ReferenceLookupDTO>> GetSaaSCompanyReferencesAsync(int companyId)
        {
            return await _context.CompanyReferences
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.Reference)
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => MapReference(x))
                .Distinct()
                .ToListAsync();
        }

        private async Task<List<ReferenceLookupDTO>> GetSaaSCenterReferencesAsync(int companyId, int centerId)
        {
            return await _context.CompanyReferences
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .Select(x => x.Reference)
                .Where(x => x.Status)
                .Where(x => x.CenterReferences.Any(cr => cr.CenterId == centerId))
                .OrderBy(x => x.Name)
                .Select(x => MapReference(x))
                .Distinct()
                .ToListAsync();
        }

        private async Task<List<ReferenceLookupDTO>> GetSaaSUserReferencesAsync(int userId)
        {
            await Task.CompletedTask;

            return new List<ReferenceLookupDTO>();
        }

        #endregion

        #region Projection Helpers

        private static ConsultantLookupDTO MapConsultant(Management.Consultant.Entities.Consultant consultant)
        {
            return new ConsultantLookupDTO
            {
                Id = consultant.Id,
                Name = consultant.Name ?? ""
            };
        }

        private static ReferenceLookupDTO MapReference(Management.Reference.Entities.Reference reference)
        {
            return new ReferenceLookupDTO
            {
                Id = reference.Id,
                Name = reference.Name ?? "",
                RateTypeId = reference.RateTypeId,
                MaxDiscount = reference.MaxDiscount,
                DefaultDiscount = reference.DefaultDiscount
            };
        }

        public async Task<Dictionary<int, List<ConsultantLookupDTO>>> GetCenterConsultantMapAsync()
        {
            var context = await _currentContextService.GetAsync();

            if (IsPureSaaS())
            {
                return await _context.CompanyConsultants
                    .AsNoTracking()
                    .Where(x => x.CompanyId == context.CompanyId)
                    .SelectMany(
                        cc => cc.Consultant.CenterConsultants,
                        (cc, center) => new
                        {
                            center.CenterId,
                            Consultant = cc.Consultant
                        })
                    .Where(x => x.Consultant.Status)
                    .GroupBy(x => x.CenterId)
                    .ToDictionaryAsync(
                        g => g.Key,
                        g => g.Select(x => MapConsultant(x.Consultant))
                            .Distinct()
                            .OrderBy(x => x.Name)
                            .ToList());
            }

            return await _context.CenterConsultants
                .AsNoTracking()
                .Where(x => x.Consultant.Status)
                .GroupBy(x => x.CenterId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(x => MapConsultant(x.Consultant))
                        .Distinct()
                        .OrderBy(x => x.Name)
                        .ToList());
        }

        public async Task<Dictionary<int, List<ReferenceLookupDTO>>> GetCenterReferenceMapAsync()
        {
            var context = await _currentContextService.GetAsync();

            if (IsPureSaaS())
            {
                return await _context.CompanyReferences
                    .AsNoTracking()
                    .Where(x => x.CompanyId == context.CompanyId)
                    .SelectMany(
                        cr => cr.Reference.CenterReferences,
                        (cr, center) => new
                        {
                            center.CenterId,
                            Reference = cr.Reference
                        })
                    .Where(x => x.Reference.Status)
                    .GroupBy(x => x.CenterId)
                    .ToDictionaryAsync(
                        g => g.Key,
                        g => g.Select(x => MapReference(x.Reference))
                            .Distinct()
                            .OrderBy(x => x.Name)
                            .ToList());
            }

            return await _context.CenterReferences
                .AsNoTracking()
                .Where(x => x.Reference.Status)
                .GroupBy(x => x.CenterId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(x => MapReference(x.Reference))
                        .Distinct()
                        .OrderBy(x => x.Name)
                        .ToList());
        }

        #endregion

        #region Helpers

        private bool IsPureSaaS()
        {
            return _deploymentSettings.GetDeploymentMode() == DeploymentMode.PureSaaS;
        }

        #endregion
    }
}