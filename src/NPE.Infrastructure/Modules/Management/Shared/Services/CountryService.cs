using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Core.Modules.Management.Shared.Services;
using NPE.Infrastructure.Common.Crud;
using NPE.Infrastructure.Modules.Management;
using NPE.Infrastructure.Modules.Management.Shared.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Management.Shared.Services
{
    public class CountryService : BaseCrudService<Country, CountryDTO, int>, ICountryService
    {
        public CountryService(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CountryDTO>> GetAllAsync()
        {
            // Return only active countries as per lookup/shared conventions
            var items = await DbSet.Where(x => x.Status == true).ToListAsync();
            return items.Select(ToDto);
        }

        public override async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("Country not found.");

            // Soft delete by setting Status = false
            entity.Status = false;
        }

        public new async Task<int> UpdateAsync(CountryDTO dto)
        {
            var entity = await DbSet.FindAsync(dto.CountryCode);

            if (entity == null)
                throw new KeyNotFoundException("Country not found.");

            UpdateEntity(entity, dto);

            return entity.CountryCode;
        }

        protected override CountryDTO ToDto(Country entity)
        {
            return CountryMapper.ToDTO(entity)!;
        }

        protected override Country ToEntity(CountryDTO dto)
        {
            var entity = CountryMapper.ToEntity(dto);

            entity.CreatedBy = string.IsNullOrWhiteSpace(entity.CreatedBy) ? "Admin" : entity.CreatedBy;
            entity.ModifiedBy = string.IsNullOrWhiteSpace(entity.ModifiedBy) ? "Admin" : entity.ModifiedBy;
            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = DateTime.Now;
            }
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.Now;
            }

            return entity;
        }

        protected override void UpdateEntity(Country entity, CountryDTO dto)
        {
            CountryMapper.MapToExisting(dto, entity);

            entity.ModifiedBy = string.IsNullOrWhiteSpace(dto.ModifiedBy) ? "Admin" : dto.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
        }

        protected override int GetKey(Country entity)
        {
            return entity.CountryCode;
        }

        protected override int GetDtoKey(CountryDTO dto)
        {
            return dto.CountryCode;
        }
    }
}
