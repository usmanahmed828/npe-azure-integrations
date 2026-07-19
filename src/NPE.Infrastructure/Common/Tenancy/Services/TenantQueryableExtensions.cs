using Microsoft.EntityFrameworkCore;

using NPE.Core.Common.Tenancy.Services;

using NPE.Infrastructure.Modules.Management.Centers;
using NPE.Infrastructure.Modules.Management.Consultant.Entities;
using NPE.Infrastructure.Modules.Management.Reference.Entities;
using NPE.Infrastructure.Modules.Patients.Entities;
using NPE.Infrastructure.Modules.Cases.Entities;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Common.Tenancy
{
    public static class TenantQueryableExtensions
    {
        public static IQueryable<Center> ApplyCenterOwnership(this IQueryable<Center> query, ApplicationDbContext context, ITenantOwnershipResolver tenant, int companyId)
        {
            if (tenant.IsHybridLegacy())
            {
                return query;
            }

            return query.Where(x => context.CompanyCenters.Any(tc => tc.CompanyId == companyId && tc.CenterId == x.Id));
        }

        public static IQueryable<Consultant> ApplyConsultantOwnership(this IQueryable<Consultant> query, ApplicationDbContext context, ITenantOwnershipResolver tenant, int companyId)
        {
            if (tenant.IsHybridLegacy())
            {
                return query;
            }

            return query.Where(x => context.CompanyConsultants.Any(tc => tc.CompanyId == companyId && tc.ConsultantId == x.Id));
        }

        public static IQueryable<Reference> ApplyReferenceOwnership(this IQueryable<Reference> query, ApplicationDbContext context, ITenantOwnershipResolver tenant, int companyId)
        {
            if (tenant.IsHybridLegacy())
            {
                return query;
            }

            return query.Where(x => context.CompanyReferences.Any(tr => tr.CompanyId == companyId && tr.ReferenceId == x.Id));
        }

        public static IQueryable<Patient> ApplyPatientOwnership(this IQueryable<Patient> query, ApplicationDbContext context, ITenantOwnershipResolver tenant, int companyId)
        {
            if (tenant.IsHybridLegacy())
            {
                return query;
            }

            return query.Where(x => context.CompanyPatients.Any(tp => tp.CompanyId == companyId && tp.PatientId == x.Id));
        }

        public static IQueryable<Case> ApplyCaseOwnership(this IQueryable<Case> query, ApplicationDbContext context, ITenantOwnershipResolver tenant, int companyId)
        {
            if (tenant.IsHybridLegacy())
            {
                return query;
            }

            return query.Where(x => context.CompanyCases.Any(tc => tc.CompanyId == companyId && tc.CaseId == x.Id));
        }

        public static IQueryable<Test> ApplyTestOwnership(this IQueryable<Test> query, ApplicationDbContext context, ITenantOwnershipResolver tenant, int companyId)
        {
            if (tenant.IsHybridLegacy())
            {
                return query;
            }

            return query.Where(x => context.CompanyTests.Any(ct => ct.CompanyId == companyId && ct.TestId == x.ID));
        }
    }
}