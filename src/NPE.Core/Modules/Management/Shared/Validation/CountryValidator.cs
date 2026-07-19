using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Shared.DTOs;
using System;

namespace NPE.Core.Modules.Management.Shared.Validation
{
    public class CountryValidator : IValidator<CountryDTO>
    {
        public void ValidateForCreate(CountryDTO dto)
        {
            ValidateCommon(dto);

            if (dto.CountryCode <= 0)
            {
                throw new InvalidOperationException("CountryCode must be greater than 0.");
            }
        }

        public void ValidateForUpdate(CountryDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("Country payload is required.");
            }

            if (dto.CountryCode <= 0)
            {
                throw new InvalidOperationException("CountryCode must be greater than 0.");
            }

            ValidateCommon(dto);
        }

        private static void ValidateCommon(CountryDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("Country payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.CountryName))
            {
                throw new InvalidOperationException("Country name is required.");
            }

            if (dto.CountryName.Length > 100)
            {
                throw new InvalidOperationException("Country name length cannot exceed 100 characters.");
            }

            if (dto.CreatedBy != null && dto.CreatedBy.Length > 30)
            {
                throw new InvalidOperationException("CreatedBy length cannot exceed 30 characters.");
            }

            if (dto.ModifiedBy != null && dto.ModifiedBy.Length > 30)
            {
                throw new InvalidOperationException("ModifiedBy length cannot exceed 30 characters.");
            }
        }
    }
}
