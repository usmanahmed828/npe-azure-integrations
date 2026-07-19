using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Core.Modules.Management.Shared.Services;
using NPE.Infrastructure.Common.Crud;
using NPE.Infrastructure.Modules.Management.Shared.Entities;
using NPE.Infrastructure.Modules.Management.Shared.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Management.Shared.Services
{
    public class PatientTitleService : BaseCrudService<PatientTitle, PatientTitleDTO, int>, IPatientTitleService
    {
        public PatientTitleService(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<PatientTitleDTO>> GetAllAsync()
        {
            // Only return active records as per standard conventions for lookup tables
            var items = await DbSet.Where(x => x.Status == true).ToListAsync();
            return items.Select(ToDto);
        }

        public override async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("PatientTitle not found.");

            // Soft delete by setting Status = false
            entity.Status = false;
        }

        public new async Task<int> UpdateAsync(PatientTitleDTO dto)
        {
            var entity = await DbSet.FindAsync(dto.Id);

            if (entity == null)
                throw new KeyNotFoundException("PatientTitle not found.");

            UpdateEntity(entity, dto);

            return entity.Id;
        }

        protected override PatientTitleDTO ToDto(PatientTitle entity)
        {
            return PatientTitleMapper.ToDTO(entity)!;
        }

        protected override PatientTitle ToEntity(PatientTitleDTO dto)
        {
            var entity = PatientTitleMapper.ToEntity(dto);

            entity.CreatedBy ??= dto.CreatedBy ?? "Admin";
            entity.ModifiedBy ??= dto.ModifiedBy ?? "Admin";
            entity.CreatedDate ??= DateTime.Now;
            entity.ModifiedDate ??= DateTime.Now;

            if (entity.Status == null)
            {
                entity.Status = true;
            }

            return entity;
        }

        protected override void UpdateEntity(PatientTitle entity, PatientTitleDTO dto)
        {
            PatientTitleMapper.MapToExisting(dto, entity);

            entity.ModifiedBy = dto.ModifiedBy ?? "Admin";
            entity.ModifiedDate = DateTime.Now;
        }

        protected override int GetKey(PatientTitle entity)
        {
            return entity.Id;
        }

        protected override int GetDtoKey(PatientTitleDTO dto)
        {
            return dto.Id;
        }
    }
}
