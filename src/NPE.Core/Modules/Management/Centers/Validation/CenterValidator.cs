using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Center.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Centers.Validation
{
    public class CenterValidator : IValidator<CenterDTO>
    {
        public void ValidateForCreate(CenterDTO dto)
        {
            ValidateCommon(dto);

            if (dto.MaxLabNumbersPerDay <= 0)
            {
                throw new InvalidOperationException("Max lab numbers per day must be greater than zero.");
            }
        }

        public void ValidateForUpdate(CenterDTO dto)
        {
            if (dto.Id <= 0)
            {
                throw new InvalidOperationException("Center id is required.");
            }

            ValidateCommon(dto);
        }

        private static void ValidateCommon(CenterDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("Center payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new InvalidOperationException("Center name is required.");
            }

            if (!dto.City.HasValue || dto.City <= 0)
            {
                throw new InvalidOperationException("City is required.");
            }

            if (!dto.Country.HasValue || dto.Country <= 0)
            {
                throw new InvalidOperationException("Country is required.");
            }
        }
    }
}
