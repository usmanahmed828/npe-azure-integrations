using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Consultant.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Consultants.Validation
{
    public class ConsultantValidator : IValidator<ConsultantDto>
    {
        public void ValidateForCreate(ConsultantDto dto)
        {
            ValidateCommon(dto);
        }

        public void ValidateForUpdate(ConsultantDto dto)
        {
            if (dto.Id <= 0)
            {
                throw new InvalidOperationException("Consultant id is required.");
            }

            ValidateCommon(dto);
        }

        private static void ValidateCommon(ConsultantDto dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("Consultant payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new InvalidOperationException("Consultant name is required.");
            }
        }
    }
}
