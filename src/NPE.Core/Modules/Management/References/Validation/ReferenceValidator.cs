using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Reference.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.References.Validation
{
    public class ReferenceValidator : IValidator<ReferenceDTO>
    {
        public void ValidateForCreate(ReferenceDTO dto)
        {
            ValidateCommon(dto);
        }

        public void ValidateForUpdate(ReferenceDTO dto)
        {
            if (dto.Id <= 0)
            {
                throw new InvalidOperationException("Reference id is required.");
            }

            ValidateCommon(dto);
        }

        private static void ValidateCommon(ReferenceDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("Reference payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new InvalidOperationException("Reference name is required.");
            }
        }
    }
}
