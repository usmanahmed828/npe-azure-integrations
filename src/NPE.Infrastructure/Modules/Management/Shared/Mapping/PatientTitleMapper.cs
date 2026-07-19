using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Infrastructure.Modules.Management.Shared.Entities;
using System;

namespace NPE.Infrastructure.Modules.Management.Shared.Mapping
{
    public static class PatientTitleMapper
    {
        public static PatientTitleDTO? ToDTO(PatientTitle? entity)
        {
            if (entity == null) return null;

            return new PatientTitleDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Sex = entity.Sex,
                Status = entity.Status,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate
            };
        }

        public static PatientTitle ToEntity(PatientTitleDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new PatientTitle
            {
                Id = dto.Id,
                Title = dto.Title,
                Sex = dto.Sex,
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
        }

        public static void MapToExisting(PatientTitleDTO dto, PatientTitle entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Title = dto.Title;
            entity.Sex = dto.Sex;
            entity.Status = dto.Status;
        }
    }
}
