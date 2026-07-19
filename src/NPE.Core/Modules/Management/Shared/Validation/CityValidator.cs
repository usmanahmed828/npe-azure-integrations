using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Shared.DTOs;
using System;

namespace NPE.Core.Modules.Management.Shared.Validation
{
    public class CityValidator : IValidator<CityDTO>
    {
        public void ValidateForCreate(CityDTO dto)
        {
            ValidateCommon(dto);

            if (dto.CityCode <= 0)
            {
                throw new InvalidOperationException("CityCode must be greater than 0.");
            }
        }

        public void ValidateForUpdate(CityDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("City payload is required.");
            }

            if (dto.CityCode <= 0)
            {
                throw new InvalidOperationException("CityCode must be greater than 0.");
            }

            ValidateCommon(dto);
        }

        private static void ValidateCommon(CityDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("City payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.CityName))
            {
                throw new InvalidOperationException("City name is required.");
            }

            if (dto.CityName.Length > 100)
            {
                throw new InvalidOperationException("City name length cannot exceed 100 characters.");
            }

            if (dto.CountryCode <= 0)
            {
                throw new InvalidOperationException("CountryCode is required and must be greater than 0.");
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
