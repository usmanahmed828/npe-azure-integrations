using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Shared.DTOs;
using System;

namespace NPE.Core.Modules.Management.Shared.Validation
{
    public class PatientTitleValidator : IValidator<PatientTitleDTO>
    {
        public void ValidateForCreate(PatientTitleDTO dto)
        {
            ValidateCommon(dto);
        }

        public void ValidateForUpdate(PatientTitleDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("PatientTitle payload is required.");
            }

            if (dto.Id <= 0)
            {
                throw new InvalidOperationException("ID is required and must be greater than 0.");
            }

            ValidateCommon(dto);
        }

        private static void ValidateCommon(PatientTitleDTO dto)
        {
            if (dto == null)
            {
                throw new InvalidOperationException("PatientTitle payload is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                throw new InvalidOperationException("Title is required.");
            }

            if (dto.Title.Length > 50)
            {
                throw new InvalidOperationException("Title length cannot exceed 50 characters.");
            }
        }
    }
}
