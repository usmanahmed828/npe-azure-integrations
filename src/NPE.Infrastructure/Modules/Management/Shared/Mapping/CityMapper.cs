using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Infrastructure.Modules.Management;
using System;

namespace NPE.Infrastructure.Modules.Management.Shared.Mapping
{
    public static class CityMapper
    {
        public static CityDTO? ToDTO(City? entity)
        {
            if (entity == null) return null;

            return new CityDTO
            {
                CityCode = entity.CityCode,
                CityName = entity.CityName,
                CountryCode = entity.CountryCode,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Status = entity.Status
            };
        }

        public static City ToEntity(CityDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new City
            {
                CityCode = dto.CityCode,
                CityName = dto.CityName,
                CountryCode = dto.CountryCode,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate,
                Status = dto.Status
            };
        }

        public static void MapToExisting(CityDTO dto, City entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.CityName = dto.CityName;
            entity.CountryCode = dto.CountryCode;
            entity.Status = dto.Status;
        }
    }
}
