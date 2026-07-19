using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Infrastructure.Modules.Management;
using System;

namespace NPE.Infrastructure.Modules.Management.Shared.Mapping
{
    public static class CountryMapper
    {
        public static CountryDTO? ToDTO(Country? entity)
        {
            if (entity == null) return null;

            return new CountryDTO
            {
                CountryCode = entity.CountryCode,
                CountryName = entity.CountryName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Status = entity.Status
            };
        }

        public static Country ToEntity(CountryDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Country
            {
                CountryCode = dto.CountryCode,
                CountryName = dto.CountryName,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate,
                Status = dto.Status
            };
        }

        public static void MapToExisting(CountryDTO dto, Country entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.CountryName = dto.CountryName;
            entity.Status = dto.Status;
        }
    }
}
