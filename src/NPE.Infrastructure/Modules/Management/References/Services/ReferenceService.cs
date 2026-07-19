using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.BusinessObjects;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Infrastructure.Common.Tenancy;
using NPE.Infrastructure.Modules.Management.Centers;
using NPE.Infrastructure.Modules.Management.Centers.Mapping;
using NPE.Infrastructure.Modules.Management.Consultant.Mapping;
using NPE.Infrastructure.Modules.Management.Reference.Mapping;

namespace NPE.Infrastructure.Modules.Management.Reference.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentContextService _currentContextService;
        private readonly ITenantOwnershipResolver _tenantOwnershipResolver;
        //private readonly IUnitOfWork _unitOfWork;

        public ReferenceService(ApplicationDbContext context, IIdentityService identityService, ICurrentContextService currentContextService, ITenantOwnershipResolver tenantOwnershipResolver)
        {
            _context = context;
            _identityService = identityService;
            _currentContextService = currentContextService;
            _tenantOwnershipResolver = tenantOwnershipResolver;
            //_unitOfWork = unitOfWork;
        }

        #region Lookup
        //public async Task<List<ReferenceLookupDTO>> GetLookupAsync()
        //{
        //    var context = await _currentContextService.GetAsync();

        //    var references = await _context.References
        //            .AsNoTracking()
        //            .Where(x => x.Status)
        //            .ApplyReferenceOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
        //            .OrderBy(x => x.Name)
        //            .Select(x => new ReferenceLookupDTO
        //            {
        //                Id = x.Id,
        //                Name = x.Name,
        //                RateTypeId = x.RateTypeId,
        //                MaxDiscount = x.MaxDiscount,
        //                DefaultDiscount = x.DefaultDiscount
        //            })

        //            .ToListAsync();

        //    return references;
        //}
        #endregion

        #region CRUD
        public async Task<IEnumerable<ReferenceDTO>> GetAllAsync()
        {
            var context = await _currentContextService.GetAsync();

            var entities = await _context.References
                    .AsNoTracking()
                    .Include(x => x.ReferenceSetting)
                    .ApplyReferenceOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .OrderBy(x => x.Name)
                    .ToListAsync();

            return entities.Select(ReferenceMapper.ToDTO).ToList();
        }
        public async Task<ReferenceDTO?> GetByIdAsync(int id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.References
                    .AsNoTracking()
                    .Include(x => x.ReferenceSetting)
                    .ApplyReferenceOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .SingleOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : ReferenceMapper.ToDTO(entity);
        }
        public async Task<int> CreateAsync(ReferenceDTO dto)
        {
            var referenceId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Reference);

            dto.Id = referenceId;

            var entity = ReferenceMapper.ToEntity(dto);

            _context.References.Add(entity);

            if (dto.Setting != null)
            {
                var setting = ReferenceMapper.ReferenceSettingToEntity(dto.Setting);

                setting.ReferenceId = referenceId;

                _context.ReferenceSettings.Add(setting);

                var entry = _context.Entry(setting);

                foreach (var p in entry.Properties)
                {
                    Console.WriteLine(
                        $"{p.Metadata.Name} = {p.CurrentValue}");
                }
            }

            //await _unitOfWork.CommitAsync();

            return entity.Id;
        }
        public async Task<ReferenceDTO> CreateAndReturnAsync(ReferenceDTO dto)
        {
            var referenceId = await CreateAsync(dto);

            var entity = await _context.References
                .AsNoTracking()
                .FirstAsync(x => x.Id == referenceId);

            return ReferenceMapper.ToDTO(entity);
        }
        public async Task<int> UpdateAsync(ReferenceDTO dto)
        {
            var entity = await _context.References
                    .Include(x => x.ReferenceSetting)
                    .SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
            {
                entity = ReferenceMapper.ToEntity(dto);
                entity.CreatedBy = dto.CreatedBy ?? "Admin";
                entity.CreatedDate = DateTime.Now;

                _context.References.Add(entity);
            }
            else
                ReferenceMapper.MapToExisting(dto, entity);

            if (dto.Setting != null)
            {
                if (entity.ReferenceSetting == null)
                {
                    entity.ReferenceSetting = ReferenceMapper.ReferenceSettingToEntity(dto.Setting);
                    entity.ReferenceSetting.ReferenceId = entity.Id;

                    _context.ReferenceSettings.Add(entity.ReferenceSetting);
                }
                else
                {
                    ReferenceMapper.MapReferenceSettingToExisting(dto.Setting, entity.ReferenceSetting);
                }
            }

            entity.ModifiedBy = dto.ModifiedBy ?? "Admin";
            entity.ModifiedDate = DateTime.Now;

            //await _unitOfWork.CommitAsync();

            return entity.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.References.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            entity.Status = false;
            entity.ModifiedDate = DateTime.Now;

            //await _unitOfWork.CommitAsync();
        }
        #endregion

        public async Task AssignReferenceToCompanyAsync(int companyId, int referenceId)
        {
            if (!_tenantOwnershipResolver.IsPureSaaS())
            {
                return;
            }

            var entity = ReferenceMapper.CompanyReferenceToEntity(companyId, referenceId);

            await _context.CompanyReferences.AddAsync(entity);

            //await _unitOfWork.CommitAsync();
        }
    }
}