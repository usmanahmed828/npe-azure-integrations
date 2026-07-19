using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Enums;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Policies;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Modules.Lookups.Services;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Centers.Services;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Infrastructure.Common.Tenancy;
using NPE.Infrastructure.Modules.Management.Centers.Mapping;

namespace NPE.Infrastructure.Modules.Management.Centers.Services
{
    public class CenterService : ICenterService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentContextService _currentContextService;
        private readonly ITenantOwnershipResolver _tenantOwnershipResolver;
        private readonly ILookupPolicyService _lookupPolicyService;
        private readonly ILookupResolverService _lookupResolverService;
        //private readonly IUnitOfWork _unitOfWork;

        public CenterService(ApplicationDbContext context, IIdentityService identityService, ICurrentContextService currentContextService, ITenantOwnershipResolver tenantOwnershipResolver, ILookupPolicyService lookupPolicyService, ILookupResolverService lookupResolverService)
        {
            _context = context;
            _identityService = identityService;
            _currentContextService = currentContextService;
            _tenantOwnershipResolver = tenantOwnershipResolver;
            _lookupPolicyService = lookupPolicyService;
            _lookupResolverService = lookupResolverService;
            //_unitOfWork = unitOfWork;
        }

        #region Lookup
        public async Task<CenterLookupBundleDTO> GetLookupAsync()
        {
            var context = await _currentContextService.GetAsync();

            var policy = await _lookupPolicyService.GetPolicyAsync(context.CompanyId);

            var centers = await _context.Centers
                    .AsNoTracking()
                    .Include(x => x.CenterSetting)
                    .Where(x => x.Status)
                    .ApplyCenterOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .OrderBy(x => x.Name)
                    .ToListAsync();

            var consultants = policy.ConsultantAccessMode == LookupAccessMode.Company ? await _lookupResolverService.GetConsultantsAsync() : new List<ConsultantLookupDTO>();

            var references = policy.ReferenceAccessMode == LookupAccessMode.Company ? await _lookupResolverService.GetReferencesAsync() : new List<ReferenceLookupDTO>();

            var centerConsultantMap = policy.ConsultantAccessMode == LookupAccessMode.Center ? await _lookupResolverService.GetCenterConsultantMapAsync() : new Dictionary<int, List<ConsultantLookupDTO>>();

            var centerReferenceMap = policy.ReferenceAccessMode == LookupAccessMode.Center ? await _lookupResolverService.GetCenterReferenceMapAsync() : new Dictionary<int, List<ReferenceLookupDTO>>();

            return BuildLookupBundle(centers, policy, consultants, references, centerConsultantMap, centerReferenceMap);

            //return BuildLookupBundle(centers);
        }
        #endregion

        #region Lookup Healper
        //private static CenterLookupBundleDTO BuildLookupBundle(List<Center> centers)
        //{
        //    return new CenterLookupBundleDTO
        //    {
        //        RegistrationLocations = centers
        //                .Select(MapLookup)
        //                .ToList(),

        //        DestinationLocations = centers
        //                .Where(x => x.IsLab)
        //                .Select(MapLookup)
        //                .ToList()
        //    };
        //}
        private static CenterLookupBundleDTO BuildLookupBundle(List<Center> centers, LookupPolicyDTO policy, List<ConsultantLookupDTO> consultants, List<ReferenceLookupDTO> references,
    Dictionary<int, List<ConsultantLookupDTO>> centerConsultantMap, Dictionary<int, List<ReferenceLookupDTO>> centerReferenceMap)
        {
            var registrationLocations = centers
                .Select(MapLookup)
                .ToList();

            if (policy.ConsultantAccessMode == LookupAccessMode.Center)
            {
                foreach (var center in registrationLocations)
                {
                    center.Consultants =
                        centerConsultantMap.TryGetValue(
                            center.Id,
                            out var centerConsultants)
                                ? centerConsultants
                                : new List<ConsultantLookupDTO>();
                }
            }

            if (policy.ReferenceAccessMode == LookupAccessMode.Center)
            {
                foreach (var center in registrationLocations)
                {
                    center.References =
                        centerReferenceMap.TryGetValue(
                            center.Id,
                            out var centerReferences)
                                ? centerReferences
                                : new List<ReferenceLookupDTO>();
                }
            }

            return new CenterLookupBundleDTO
            {
                RegistrationLocations = registrationLocations,

                DestinationLocations = centers
                    .Where(x => x.IsLab)
                    .Select(MapLookup)
                    .ToList(),

                Consultants = consultants,

                References = references,

                Policy = policy
            };
        }
        private static CenterLookupDTO MapLookup(Center center)
        {
            return new CenterLookupDTO
            {
                Id = center.Id,
                Name = center.Name ?? "",
                IsLab = center.IsLab,
                DestinationLocation = center.CenterSetting?.DestinationLocation,
                DefaultStatus = center.CenterSetting?.DefaultStatus,
                IsCreditEnabled = center.CenterSetting?.IsCreditFeatureEnabled ?? false,
                CreditLimit = center.CreditLimit
            };
        }
        #endregion

        #region CRUD
        public async Task<CenterDTO?> GetByIdAsync(int id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await BuildCenterQuery()
                .ApplyCenterOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : CenterMapper.ToDTO(entity);
        }
        public async Task<IEnumerable<CenterDTO>> GetAllAsync()
        {
            var context = await _currentContextService.GetAsync();

            var entities = await BuildCenterQuery()
                .ApplyCenterOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync();

            return entities
                .Select(CenterMapper.ToDTO)
                .ToList();
        }
        public async Task<int> CreateAsync(CenterDTO dto)
        {
            var centerId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Center);

            dto.Id = centerId;

            var entity = CenterMapper.ToEntity(dto);

            _context.Centers.Add(entity);

            if (dto.Setting != null)
            {
                dto.Setting.CenterId = centerId;
                var setting = CenterMapper.CenterSettingToEntity(dto.Setting);
                _context.CenterSettings.Add(setting);
            }

            //await _unitOfWork.CommitAsync();
            await InitializeCenterLabNumberAsync(centerId, dto.MaxLabNumbersPerDay);

            return entity.Id;
        }

        //public async Task<CenterDTO> CreateAndReturnAsync(CenterDTO dto)
        //{
        //    var centerId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Center);

        //    dto.Id = centerId;

        //    var entity = CenterMapper.ToEntity(dto);

        //    _context.Centers.Add(entity);

        //    if (dto.Setting != null)
        //    {
        //        dto.Setting.CenterId = centerId;
        //        var setting = CenterMapper.CenterSettingToEntity(dto.Setting);
        //        _context.CenterSettings.Add(setting);
        //    }

        //    //await _unitOfWork.CommitAsync();

        //    return CenterMapper.ToDTO(entity);
        //}
        public async Task<CenterDTO> CreateAndReturnAsync(CenterDTO dto)
        {
            var centerId = await CreateAsync(dto);

            var entity = await BuildCenterQuery()
                .AsNoTracking()
                .FirstAsync(x => x.Id == centerId);

            return CenterMapper.ToDTO(entity);
        }
        public async Task<int> UpdateAsync(CenterDTO dto)
        {
            var entity = await _context.Centers.FindAsync(dto.Id);

            //if (entity == null)
            //{
            //    entity = CenterMapper.ToEntity(dto);
            //    _context.Centers.Add(entity);

            //    //await _unitOfWork.CommitAsync();
            //    if (dto.Setting != null)
            //    {
            //        dto.Setting.CenterId = dto.Id;

            //        var newSetting = CenterMapper.CenterSettingToEntity(dto.Setting);

            //        _context.CenterSettings.Add(newSetting);
            //    }

            //    await EnsureCenterLabNumberExistsAsync(dto.Id, dto.MaxLabNumbersPerDay);

            //    return entity.Id;
            //}
            if (entity == null)
            {
                return dto.Id;
            }

            CenterMapper.MapToExisting(dto, entity);

            entity.ModifiedBy = dto.ModifiedBy ?? "Administrator";
            entity.ModifiedDate = DateTime.Now;

            if (dto.Setting != null)
            {
                var setting = await _context.CenterSettings.FirstOrDefaultAsync(x => x.CenterId == dto.Id);

                if (setting == null)
                {
                    dto.Setting.CenterId = dto.Id;

                    setting = CenterMapper.CenterSettingToEntity(dto.Setting);

                    _context.CenterSettings.Add(setting);
                }
                else
                {
                    CenterMapper.MapCenterSettingToExisting(dto.Setting, setting);
                }
            }

            //await _unitOfWork.CommitAsync();
            await EnsureCenterLabNumberExistsAsync(dto.Id, dto.MaxLabNumbersPerDay);

            return entity.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Centers.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            entity.Status = false;
            entity.ModifiedDate = DateTime.Now;

            //await _unitOfWork.CommitAsync();
        }
        #endregion

        #region Save
        public async Task AssignCenterToCompanyAsync(int companyId, int centerId, bool isRootCenter)
        {
            if (!_tenantOwnershipResolver.IsPureSaaS())
            {
                return;
            }

            var entity = CenterMapper.CompanyCenterToEntity(companyId, centerId, isRootCenter);

            await _context.CompanyCenters.AddAsync(entity);

            //await _unitOfWork.CommitAsync();
        }
        public async Task SaveCenterReference(int centerId, int referenceId)
        {
            var entity = CenterMapper.CenterReferenceToEntity(centerId, referenceId);

            await _context.CenterReferences.AddAsync(entity);
        }
        public async Task SaveCenterConsultant(int centerId, int consultantId)
        {
            var entity = CenterMapper.CenterConsultantToEntity(centerId, consultantId);

            await _context.CenterConsultants.AddAsync(entity);
        }
        public async Task SaveCenterAdditionalData(int centerId, int consultantId, int referenceId)
        {
            var entities = CenterMapper.CenterAdditionalDataToEntity(centerId, consultantId, referenceId);

            await _context.CenterAdditionalDatas.AddRangeAsync(entities);
        }
        #endregion

        #region Helpers
        private IQueryable<Center> BuildCenterQuery()
        {
            return _context.Centers
                .Include(x => x.CenterSetting)
                .Include(x => x.CenterCreditSummary)
                .Include(x => x.CenterAdditionalDatas)
                .Include(x => x.CenterConsultants).ThenInclude(x => x.Consultant)
                .Include(x => x.CenterReferences).ThenInclude(x => x.Reference)
                .Include(x => x.CenterLabNo);
        }
        #endregion

        private async Task InitializeCenterLabNumberAsync(int centerId, int maxLabNumbersPerDay)
        {
            if (maxLabNumbersPerDay <= 0)
            {
                throw new InvalidOperationException("MaxLabNumbersPerDay must be greater than zero.");
            }

            var maxLabNo = centerId + maxLabNumbersPerDay;

            var existing = await _context.CenterLabNos
                .SingleOrDefaultAsync(x => x.CenterCode == centerId);

            if (existing == null)
            {
                _context.CenterLabNos.Add(new CenterLabNo
                {
                    CenterCode = centerId,
                    MaxLabNo = maxLabNo
                });
            }
            else
            {
                existing.MaxLabNo = maxLabNo;
            }

            await UpdateSharedCenterIdentityAsync(maxLabNo);
        }
        private async Task EnsureCenterLabNumberExistsAsync(int centerId, int maxLabNumbersPerDay)
        {
            if (maxLabNumbersPerDay <= 0)
            {
                return;
            }

            var existing = await _context.CenterLabNos
                .SingleOrDefaultAsync(x => x.CenterCode == centerId);

            if (existing != null)
            {
                return;
            }

            _context.CenterLabNos.Add(new CenterLabNo
            {
                CenterCode = centerId,
                MaxLabNo = centerId + maxLabNumbersPerDay
            });
        }
        private async Task UpdateSharedCenterIdentityAsync(int currentValue)
        {
            var identity = await _context.Identities
                .SingleOrDefaultAsync(x =>
                    x.CenterCode == 1001 &&
                    x.Type == IdentityTypes.Center);

            if (identity == null)
            {
                throw new InvalidOperationException($"Identity not configured. CenterCode=1001, Type={IdentityTypes.Center}");
            }

            identity.CurrentValue = currentValue;
        }
    }
}