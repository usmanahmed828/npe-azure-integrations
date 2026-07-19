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
    public class CityService : BaseCrudService<City, CityDTO, int>, ICityService
    {
        public CityService(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CityDTO>> GetAllAsync()
        {
            // Return only active cities as per lookup/shared conventions
            var items = await DbSet.Where(x => x.Status == true).ToListAsync();
            return items.Select(ToDto);
        }

        public async Task<List<CityDTO>> GetByCountryAsync(int countryCode)
        {
            var items = await DbSet
                .Where(x => x.CountryCode == countryCode && x.Status == true)
                .OrderBy(x => x.CityName)
                .ToListAsync();

            return items.Select(ToDto).ToList();
        }

        public override async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("City not found.");

            // Soft delete by setting Status = false
            entity.Status = false;
        }

        public new async Task<int> UpdateAsync(CityDTO dto)
        {
            var entity = await DbSet.FindAsync(dto.CityCode);

            if (entity == null)
                throw new KeyNotFoundException("City not found.");

            UpdateEntity(entity, dto);

            return entity.CityCode;
        }

        protected override CityDTO ToDto(City entity)
        {
            return CityMapper.ToDTO(entity)!;
        }

        protected override City ToEntity(CityDTO dto)
        {
            var entity = CityMapper.ToEntity(dto);

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

        protected override void UpdateEntity(City entity, CityDTO dto)
        {
            CityMapper.MapToExisting(dto, entity);

            entity.ModifiedBy = string.IsNullOrWhiteSpace(dto.ModifiedBy) ? "Admin" : dto.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
        }

        protected override int GetKey(City entity)
        {
            return entity.CityCode;
        }

        protected override int GetDtoKey(CityDTO dto)
        {
            return dto.CityCode;
        }
    }
}
