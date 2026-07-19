using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Cases.Models;
using NPE.Core.Modules.Patients.Views;
using NPE.Core.Modules.Tests.Models;
using NPE.Infrastructure.Common.Identity.Entities;
using NPE.Infrastructure.Modules.Auth.Entities;
using NPE.Infrastructure.Modules.Cases.Entities;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.Management;
using NPE.Infrastructure.Modules.Management.Centers;
using NPE.Infrastructure.Modules.Management.Consultant.Entities;
using NPE.Infrastructure.Modules.Management.Reference.Entities;
using NPE.Infrastructure.Modules.Management.Shared.Entities;
using NPE.Infrastructure.Modules.Patients.Entities;
using NPE.Infrastructure.Modules.Tenancy;
using NPE.Infrastructure.Modules.Tenancy.Entities;
using NPE.Infrastructure.Modules.Tests.Entities;

using NPE.Infrastructure.Modules.Users.Entities;

namespace NPE.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    #region Auth

    public DbSet<ExternalApp> ExternalApps => Set<ExternalApp>();
    public DbSet<ExternalAppPermission> ExternalAppsPermissions => Set<ExternalAppPermission>();

    #endregion

    #region Patients

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<PatientDetail> PatientDetails => Set<PatientDetail>();
    public DbSet<PatientSetting> PatientSettings => Set<PatientSetting>();
    public DbSet<PatientCorporateInfo> PatientCorporateInfos => Set<PatientCorporateInfo>();

    #endregion

    #region Cases

    public DbSet<Case> Cases => Set<Case>();
    public DbSet<CaseDetail> CaseDetails => Set<CaseDetail>();
    public DbSet<CasePayment> CasePayments => Set<CasePayment>();

    public DbSet<CaseAdditionalSetting> CaseAdditionalSettings
        => Set<CaseAdditionalSetting>();

    public DbSet<CaseClinicalDetail> CaseClinicalDetails
        => Set<CaseClinicalDetail>();

    public DbSet<CaseDetailInstrument> CaseDetailInstruments
        => Set<CaseDetailInstrument>();

    public DbSet<CaseInfo> CaseInfos
        => Set<CaseInfo>();

    public DbSet<CasePaymentOnline> CasePaymentOnlines
        => Set<CasePaymentOnline>();

    public DbSet<CaseRemark> CaseRemarks
        => Set<CaseRemark>();

    public DbSet<CaseSetting> CaseSettings
        => Set<CaseSetting>();

    public DbSet<CorporatePaymentFinancial> CorporatePaymentFinancials
        => Set<CorporatePaymentFinancial>();

    public DbSet<OutsourceCaseDetail> OutsourceCaseDetails
        => Set<OutsourceCaseDetail>();

    #endregion

    #region Tests

    public DbSet<Test> Tests => Set<Test>();
    public DbSet<TestDepartment> TestDepartments => Set<TestDepartment>();
    public DbSet<TestDetail> TestDetails => Set<TestDetail>();
    public DbSet<TestGroup> TestGroups => Set<TestGroup>();
    public DbSet<TestInstrumentSetting> TestInstrumentSettings => Set<TestInstrumentSetting>();
    public DbSet<TestNormalValue> TestNormalValues => Set<TestNormalValue>();
    public DbSet<TestNormalValueGraph> TestNormalValueGraph => Set<TestNormalValueGraph>();
    public DbSet<TestParameter> TestParameters => Set<TestParameter>();
    public DbSet<TestParameterNormalValue> TestParameterNormalValues => Set<TestParameterNormalValue>();
    public DbSet<TestProfile> TestProfiles => Set<TestProfile>();
    public DbSet<TestSetting> TestSettings => Set<TestSetting>();
    public DbSet<TestTemplate> TestTemplates => Set<TestTemplate>();

    public DbSet<LISSpecimenType> LISSpecimenType => Set<LISSpecimenType>();
    public DbSet<LISSpecimenSetting> LISSpecimenSetting => Set<LISSpecimenSetting>();
    #endregion

    #region Management

    #region Center
    public DbSet<Center> Centers => Set<Center>();
    public DbSet<CenterSetting> CenterSettings => Set<CenterSetting>();
    public DbSet<MaxLabNo> MaxLabNos => Set<MaxLabNo>();
    public DbSet<CenterLabNo> CenterLabNos => Set<CenterLabNo>();
    public DbSet<CenterReference> CenterReferences => Set<CenterReference>();
    public DbSet<CenterConsultant> CenterConsultants => Set<CenterConsultant>();
    public DbSet<CenterAdditionalData> CenterAdditionalDatas => Set<CenterAdditionalData>();

    //Newly added entities for Center management module, will be moved to separate module in future.
    public DbSet<CenterAdditionalInfo> CenterAdditionalInfos => Set<CenterAdditionalInfo>();

    public DbSet<CenterCreditDetail> CenterCreditDetails => Set<CenterCreditDetail>();

    public DbSet<CenterCreditHistory> CenterCreditHistories => Set<CenterCreditHistory>();

    public DbSet<CenterCreditPayment> CenterCreditPayments => Set<CenterCreditPayment>();

    public DbSet<CenterCreditSummary> CenterCreditSummaries => Set<CenterCreditSummary>();

    public DbSet<CenterOwner> CenterOwners => Set<CenterOwner>();

    public DbSet<CenterOwnerCenter> CenterOwnerCenters => Set<CenterOwnerCenter>();

    public DbSet<CenterPayment> CenterPayments => Set<CenterPayment>();

    public DbSet<CenterReportLayoutMapping> CenterReportLayoutMappings => Set<CenterReportLayoutMapping>();

    public DbSet<CentersToCheckDueAmountFor> CentersToCheckDueAmountFors => Set<CentersToCheckDueAmountFor>();
    #endregion

    #region Reference
    public DbSet<Reference> References => Set<Reference>();
    public DbSet<ReferenceSetting> ReferenceSettings => Set<ReferenceSetting>();

    //Newly added entities for reference management module, will be moved to separate module in future.
    public DbSet<ReferenceAgrement> ReferenceAgrements => Set<ReferenceAgrement>();
    public DbSet<ReferenceBillSetting> ReferenceBillSettings => Set<ReferenceBillSetting>();
    public DbSet<ReferencePayment> ReferencePayments => Set<ReferencePayment>();
    #endregion

    #region Consultant
    public DbSet<Consultant> Consultants => Set<Consultant>();
    public DbSet<ConsultantSetting> ConsultantSettings => Set<ConsultantSetting>();
    //Newly added entities for consultant management module, will be moved to separate module in future.
    public DbSet<ConsultantCommsionSetting> ConsultantCommsionSettings => Set<ConsultantCommsionSetting>();
    #endregion

    #region Shared
    public DbSet<Identity> Identities => Set<Identity>();
    public DbSet<KeyValue> KeyValue => Set<KeyValue>();
    //Newly added entities for shared management module, will be moved to separate module in future.
    public DbSet<City> Cities => Set<City>();
    public DbSet<CityLocation> CityLocations => Set<CityLocation>();
    public DbSet<ClinicalDetail> ClinicalDetails => Set<ClinicalDetail>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<LabLocation> LabLocations => Set<LabLocation>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Region> Regions => Set<Region>();
    public DbSet<Remark> Remarks => Set<Remark>();
    public DbSet<RemarkSetting> RemarkSettings => Set<RemarkSetting>();
    public DbSet<PatientTitle> PatientTitles => Set<PatientTitle>();
    #endregion

    #endregion

    #region iLock

    public DbSet<ILockApplication> ILockApplications => Set<ILockApplication>();
    public DbSet<ILockCompany> ILockCompanies => Set<ILockCompany>();
    public DbSet<ILockGlobalDef> ILockGlobalDefs => Set<ILockGlobalDef>();
    public DbSet<ILockGroup> ILockGroups => Set<ILockGroup>();
    public DbSet<ILockGroupUIObject> ILockGroupUIObjects => Set<ILockGroupUIObject>();
    public DbSet<ILockGroupUser> ILockGroupUsers => Set<ILockGroupUser>();
    public DbSet<ILockIdentity> ILockIdentities => Set<ILockIdentity>();
    public DbSet<ILockLocation> ILockLocations => Set<ILockLocation>();
    public DbSet<ILockUIContainer> ILockUicontainers => Set<ILockUIContainer>();
    public DbSet<ILockUIObject> ILockUiobjects => Set<ILockUIObject>();
    public DbSet<ILockUser> ILockUsers => Set<ILockUser>();
    public DbSet<ILockUserAudit> ILockUserAudits => Set<ILockUserAudit>();
    public DbSet<ILockUserHistory> ILockUserHistories => Set<ILockUserHistory>();
    public DbSet<ILockUserLocation> ILockUserLocations => Set<ILockUserLocation>();
    public DbSet<ILockWeb> ILockWebs => Set<ILockWeb>();

    #endregion

    #region Users
    public DbSet<WebSettings> WebSettings => Set<WebSettings>();
    public DbSet<UserWebSettings> UserWebSettings => Set<UserWebSettings>();
    #endregion

    #region Read Only DTOs

    public DbSet<SearchPatientListDTO> SimplePatientCaseInfo
        => Set<SearchPatientListDTO>();

    public DbSet<TestRateResultDto> TestRateResults
        => Set<TestRateResultDto>();

    public DbSet<TestRateLookupDto> TestRateLookupDtos
        => Set<TestRateLookupDto>();

    public DbSet<PatientListView> PatientListView => Set<PatientListView>();

    #endregion

    #region Tenancy
    public DbSet<CompanyCenter> CompanyCenters => Set<CompanyCenter>();
    public DbSet<CompanyConsultant> CompanyConsultants => Set<CompanyConsultant>();
    public DbSet<CompanyReference> CompanyReferences => Set<CompanyReference>();
    public DbSet<CompanyPatient> CompanyPatients => Set<CompanyPatient>();
    public DbSet<CompanyCase> CompanyCases => Set<CompanyCase>();
    public DbSet<CompanyTest> CompanyTests => Set<CompanyTest>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        ConfigureReadOnlyDtos(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureReadOnlyDtos(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SearchPatientListDTO>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

        modelBuilder.Entity<TestRateResultDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

        modelBuilder.Entity<TestRateLookupDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

        modelBuilder.Entity<PatientListView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("PatientListView");
        });
    }
}