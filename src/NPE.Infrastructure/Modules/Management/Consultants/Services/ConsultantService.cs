using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Identity;
using NPE.Core.Common.Tenancy.Services;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Consultant.BusinessObjects;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Infrastructure.Common.Tenancy;
using NPE.Infrastructure.Modules.Management.Centers;
using NPE.Infrastructure.Modules.Management.Centers.Mapping;
using NPE.Infrastructure.Modules.Management.Consultant.Mapping;
using NPE.Infrastructure.Modules.Management.Reference.Mapping;

namespace NPE.Infrastructure.Modules.Management.Consultant.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentContextService _currentContextService;
        private readonly ITenantOwnershipResolver _tenantOwnershipResolver;
        //private readonly IUnitOfWork _unitOfWork;

        public ConsultantService(
            ApplicationDbContext context, IIdentityService identityService,
            ICurrentContextService currentContextService, ITenantOwnershipResolver tenantOwnershipResolver)
        {
            _context = context;
            _identityService = identityService;
            _currentContextService = currentContextService;
            _tenantOwnershipResolver = tenantOwnershipResolver;
            //_unitOfWork = unitOfWork;
        }

        #region Lookup

        //public async Task<List<ConsultantLookupDTO>> GetLookupAsync()
        //{
        //    var context = await _currentContextService.GetAsync();

        //    var consultants = await _context.Consultants
        //            .AsNoTracking()
        //            .Where(x => x.Status)
        //            .ApplyConsultantOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
        //            .OrderBy(x => x.Name)
        //            .Select(x => new ConsultantLookupDTO
        //                {
        //                    Id = x.Id,
        //                    Code = x.Code,
        //                    Name = x.Name
        //                })
        //            .ToListAsync();

        //    return consultants;
        //}

        #endregion

        #region CRUD
        public async Task<IEnumerable<ConsultantDto>> GetAllAsync()
        {
            var context = await _currentContextService.GetAsync();

            var entities = await _context.Consultants
                    .AsNoTracking()
                    .Include(x => x.ConsultantSetting)
                    .ApplyConsultantOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .OrderBy(x => x.Name)
                    .ToListAsync();

            return entities.Select(ConsultantMapper.ToDTO).ToList();
        }
        public async Task<ConsultantDto?> GetByIdAsync(int id)
        {
            var context = await _currentContextService.GetAsync();

            var entity = await _context.Consultants
                    .AsNoTracking()
                    .Include(x => x.ConsultantSetting)
                    .ApplyConsultantOwnership(_context, _tenantOwnershipResolver, context.CompanyId)
                    .SingleOrDefaultAsync(x => x.Id == id);

            return entity == null ? null : ConsultantMapper.ToDTO(entity);
        }
        public async Task<int> CreateAsync(ConsultantDto dto)
        {
            var consultantId = await _identityService.ConsumeAsync<int>(1001, IdentityTypes.Consultant);

            dto.Id = consultantId;

            var entity = ConsultantMapper.ToEntity(dto);

            _context.Consultants.Add(entity);

            if (dto.Setting != null)
            {
                dto.Setting.ConsultantId = consultantId;

                var setting = ConsultantMapper.ConsultantSettingToEntity(dto.Setting);

                _context.ConsultantSettings.Add(setting);
            }

            //await _unitOfWork.CommitAsync();

            return entity.Id;
        }
        public async Task<ConsultantDto> CreateAndReturnAsync(ConsultantDto dto)
        {
            var consultantId = await CreateAsync(dto);

            var entity = await _context.Consultants
                .AsNoTracking()
                .FirstAsync(x => x.Id == consultantId);

            return ConsultantMapper.ToDTO(entity);
        }
        public async Task<int> UpdateAsync(ConsultantDto dto)
        {
            var entity = await _context.Consultants
                    .Include(x => x.ConsultantSetting)
                    .SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
            {
                entity = ConsultantMapper.ToEntity(dto);
                entity.CreatedBy = dto.CreatedBy ?? "Admin";
                entity.CreatedDate = DateTime.Now;

                _context.Consultants.Add(entity);
            }
            else
                ConsultantMapper.MapToExisting(dto, entity);

            if (dto.Setting != null)
            {
                if (entity.ConsultantSetting == null)
                {
                    entity.ConsultantSetting = ConsultantMapper.ConsultantSettingToEntity(dto.Setting);
                    entity.ConsultantSetting.ConsultantId = entity.Id;

                    _context.ConsultantSettings.Add(entity.ConsultantSetting);
                }
                else
                {
                    ConsultantMapper.MapConsultantSettingToExisting(dto.Setting, entity.ConsultantSetting);
                }
            }

            //await _unitOfWork.CommitAsync();

            return entity.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Consultants.SingleAsync(x => x.Id == id);

            entity.Status = false;

            entity.ModifiedDate = DateTime.Now;

            //await _unitOfWork.CommitAsync();
        }

        #endregion

        public async Task AssignConsultantToCompanyAsync(int companyId, int consultantId)
        {
            if (!_tenantOwnershipResolver.IsPureSaaS())
            {
                return;
            }

            var entity = ConsultantMapper.CompanyConsultantToEntity(companyId, consultantId);

            await _context.CompanyConsultants.AddAsync(entity);

             //_unitOfWork.CommitAsync();
        }
    }
}