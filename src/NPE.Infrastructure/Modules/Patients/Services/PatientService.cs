using Microsoft.EntityFrameworkCore;
using NPE.Core.Common;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Modules.Cases.Models;
using NPE.Core.Modules.Patients.BusinessObjects;
using NPE.Core.Modules.Patients.Models;
using NPE.Infrastructure.Common.Crud;
using NPE.Infrastructure.Common.Data;
using NPE.Infrastructure.Common.Tenancy;
using NPE.Infrastructure.Modules.Patients.Entities;
using NPE.Infrastructure.Modules.Patients.Mapping;

namespace NPE.Infrastructure.Modules.Patients.Services
{
    public class PatientService : BaseCrudService<Patient, PatientDTO, long>, IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IPatientNumberService _patientNumberService;
        private readonly IStoredProcedureExecutor _spExecutor;
        private readonly ICurrentContextService _currentContextService;
        private readonly ITenantOwnershipResolver _tenantOwnershipResolver;

        public PatientService(
            ApplicationDbContext context,
            IPatientNumberService patientNumberService,
            IIdentityService identityService,
            IStoredProcedureExecutor spExecutor,
            ICurrentContextService currentContextService,
            ITenantOwnershipResolver tenantOwnershipResolver
            ) : base(context)
        {
            _context = context;
            _identityService = identityService;
            _patientNumberService = patientNumberService;
            _spExecutor = spExecutor;
            _currentContextService = currentContextService;
            _tenantOwnershipResolver = tenantOwnershipResolver;
        }

        #region CRUD

        public override async Task<PatientDTO?> GetByIdAsync(long id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Patients
                .Include(x => x.PatientDetail)
                .Include(x => x.PatientSetting)
                .Include(x => x.PatientCorporateInfo)
                .ApplyPatientOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : PatientMapper.ToCore(entity);
        }

        public override async Task<IEnumerable<PatientDTO>> GetAllAsync()
        {
            var context = await _currentContextService.GetAsync();

            var entities = await _context.Patients
                .Include(x => x.PatientDetail)
                .Include(x => x.PatientSetting)
                .Include(x => x.PatientCorporateInfo)
                .ApplyPatientOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .ToListAsync();

            return entities
                .Select(PatientMapper.ToCore)
                .Where(x => x != null)!;
        }

        public override async Task<long> CreateAsync(PatientDTO dto)
        {
            var patientId = await _identityService.ConsumeAsync<long>(
                dto.Location,
                IdentityTypes.Patient);

            dto.Id = patientId;

            dto.PatientNumber =
                await _patientNumberService.GenerateAsync(
                    dto.Location,
                    patientId);

            dto.ModifiedBy = dto.CreatedBy;

            var entity = PatientMapper.ToEntity(dto);
            //entity.Id = patientId;

            _context.Patients.Add(entity);

            if (_tenantOwnershipResolver.IsPureSaaS())
            {
                var context = await _currentContextService.GetAsync();

                _context.CompanyPatients.Add(PatientMapper.CompanyPatientToEntity(context.CompanyId, patientId));
            }

            return patientId;
        }

        public override async Task<PatientDTO> CreateAndReturnAsync(PatientDTO dto)
        {
            await CreateAsync(dto);

            return dto;
        }

        public override async Task<long> UpdateAsync(PatientDTO dto)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Patients
                .Include(x => x.PatientDetail)
                .Include(x => x.PatientSetting)
                .Include(x => x.PatientCorporateInfo)
                .ApplyPatientOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                throw new KeyNotFoundException("Patient not found.");

            dto.ModifiedDate = DateTime.Now;
            dto.Id = entity.Id;
            PatientMapper.UpdateEntity(entity, dto);


            return entity.Id;
        }

        public override async Task DeleteAsync(long id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Patients
                .ApplyPatientOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new KeyNotFoundException("Patient not found.");

            _context.Patients.Remove(entity);
        }

        #endregion

        #region SEARCH

        public async Task<PagedResult<SearchPatientListDTO>> SearchPatientListAsync(CaseSearchParamsDto input)
        {
            var data = await _spExecutor.ExecuteAsync<SearchPatientListDTO>("dbo.cproc_LoadPatinetCaseInfoForSimplePatientListForUser", cmd =>
                    {
                        DbHelper.AddParam(cmd, "@PatientNumber", input.PatientNumber);
                        DbHelper.AddParam(cmd, "@PatientName", input.PatientName);
                        DbHelper.AddParam(cmd, "@Sex", input.Sex);
                        DbHelper.AddParam(cmd, "@BloodGroup", input.BloodGroup);
                        DbHelper.AddParam(cmd, "@Phone", input.Phone);
                        DbHelper.AddParam(cmd, "@NIC", input.NIC);
                        DbHelper.AddParam(cmd, "@RegistrationCenter", input.RegistrationCenter);
                        DbHelper.AddParam(cmd, "@RegistrationDateFrom", input.RegistrationDateFrom);
                        DbHelper.AddParam(cmd, "@RegistrationDateTo", input.RegistrationDateTo);
                        DbHelper.AddParam(cmd, "@FilterByDate", input.FilterByDate);
                        DbHelper.AddParam(cmd, "@CaseNumber", input.CaseNumber);
                        DbHelper.AddParam(cmd, "@BankCases", input.BankCases);
                        DbHelper.AddParam(cmd, "@CaseRegLocation", input.CaseRegLocation);
                        DbHelper.AddParam(cmd, "@ConsultantID", input.ConsultantID);
                        DbHelper.AddParam(cmd, "@ReferenceID", input.ReferenceID);
                        DbHelper.AddParam(cmd, "@CaseRegFromDate", input.CaseRegFromDate);
                        DbHelper.AddParam(cmd, "@CaseRegToDate", input.CaseRegToDate);
                        DbHelper.AddParam(cmd, "@CaseRegFilterByDate", input.CaseRegFilterByDate);
                        DbHelper.AddParam(cmd, "@UserName", input.UserName);
                        DbHelper.AddParam(cmd, "@MRNo", input.MRNo);
                        DbHelper.AddParam(cmd, "@CABGNo", input.CABGNo);
                        DbHelper.AddParam(cmd, "@UserID", input.UserID);
                        DbHelper.AddParam(cmd, "@CardNumber", input.CardNumber);
                    });

            var total = data.Count;

            var paged = data
                .Skip((input.Page - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            return new PagedResult<SearchPatientListDTO>
            {
                Items = paged,
                TotalCount = total,
                Page = input.Page,
                PageSize = input.PageSize
            };
        }

        public async Task<PagedResult<PatientSearchDTO>> SearchAsync(PatientSearchRequest request)
        {
            request.PageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            request.PageSize = request.PageSize <= 0 ? 25 : request.PageSize;

            if (request.PageSize > 100)
                request.PageSize = 100;

            var query = _context.PatientListView.AsNoTracking().AsQueryable();

            if (_tenantOwnershipResolver.IsPureSaaS())
            {
                var context = await _currentContextService.GetAsync();

                query = query.Where(x => _context.CompanyPatients.Any(cp => cp.CompanyId == context.CompanyId && cp.PatientId == x.Id));
            }

            if (!string.IsNullOrWhiteSpace(request.PatientNumber))
            {
                query = query.Where(x => x.PatientNumber.Contains(request.PatientNumber));
            }

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                query = query.Where(x => (x.FirstName + " " + x.LastName).Contains(request.FullName));
            }

            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                query = query.Where(x => x.Phone!.Contains(request.Phone));
            }

            if (!string.IsNullOrWhiteSpace(request.Nic))
            {
                query = query.Where(x => x.Nic!.Contains(request.Nic));
            }

            if (!string.IsNullOrWhiteSpace(request.CardNumber))
            {
                query = query.Where(x => x.CardNumber!.Contains(request.CardNumber));
            }

            if (request.Location.HasValue)
            {
                query = query.Where(x => x.Location == request.Location.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PatientSearchDTO
                {
                    Id = x.Id,
                    PatientNumber = x.PatientNumber,
                    FullName = x.FirstName + " " + x.LastName,
                    FhName = x.Fhname,
                    AgeSex = x.AgeSex,
                    Phone = x.Phone ?? "",
                    Center = x.Center,
                    CardNumber = x.CardNumber,
                    Nic = x.Nic,
                    Email = x.Email
                })
                .ToListAsync();

            return new PagedResult<PatientSearchDTO>
            {
                Items = items,
                TotalCount = totalCount,
                Page = request.PageNo,
                PageSize = request.PageSize
            };
        }

        //public async Task<List<PatientSearchResult>>
        //    SearchAsync(PatientSearchRequest request)
        //{
        //    var query = _context.Patients
        //        .AsNoTracking()
        //        .Where(x => x.Mobile == request.Mobile);

        //    if (!string.IsNullOrWhiteSpace(request.Name))
        //    {
        //        query = query.Where(x =>
        //            x.FirstName.Contains(request.Name) ||
        //            x.LastName.Contains(request.Name));
        //    }

        //    return await query
        //        .Select(x => new PatientSearchResult
        //        {
        //            Id = x.Id,
        //            PatientNumber = x.PatientNumber,
        //            FullName = x.FirstName + " " + x.LastName,
        //            Mobile = x.Mobile,
        //            DateOfBirth = x.DateOfBirth
        //        })
        //        .ToListAsync();
        //}

        //public async Task<PagedResult<PatientInfoDto>>
        //    SearchByMobileAsync(
        //        PatientSearchParamsDto search)
        //{
        //    var query = _context.Patients
        //        .AsNoTracking()
        //        .Where(x =>
        //            !string.IsNullOrWhiteSpace(search.Mobile) &&
        //            x.Mobile.Contains(search.Mobile))
        //        .Select(x => new PatientInfoDto
        //        {
        //            MedicalRecordNumber = x.MedicalRecordNo,
        //            PatientID = x.Id,
        //            PatientNumber = x.PatientNumber,
        //            FullName = x.FirstName + " " + x.LastName,
        //            Mobile = x.Mobile,
        //            DateOfBirth = x.DateOfBirth,
        //            Age = EF.Functions.DateDiffYear(
        //                x.DateOfBirth,
        //                DateTime.Now),
        //            Sex = x.Sex == 0
        //                ? "Male"
        //                : x.Sex == 1
        //                    ? "Female"
        //                    : "Unknown",
        //            CaseNumber = "",
        //            RecentVisit = ""
        //        });

        //    var total = await query.CountAsync();

        //    var items = await query
        //        .Skip((search.Page - 1) * search.PageSize)
        //        .Take(search.PageSize)
        //        .ToListAsync();

        //    return new PagedResult<PatientInfoDto>
        //    {
        //        Items = items,
        //        TotalCount = total,
        //        Page = search.Page,
        //        PageSize = search.PageSize
        //    };
        //}

        #endregion

        #region Mapper Hooks

        protected override PatientDTO ToDto(Patient entity) => PatientMapper.ToCore(entity)!;

        protected override Patient ToEntity(PatientDTO dto) => PatientMapper.ToEntity(dto);

        protected override void UpdateEntity(Patient entity, PatientDTO dto) => PatientMapper.UpdateEntity(entity, dto);

        protected override long GetKey(Patient entity) => entity.Id;

        protected override long GetDtoKey(PatientDTO dto) => dto.Id;

        #endregion
    }
}