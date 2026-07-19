using NPE.Core.Common.Validation;
using NPE.Core.Modules.Laboratory.TestHistory.DTOs;

namespace NPE.Core.Modules.Laboratory.TestHistory.Validators
{
    public static class TestHistoryValidator
    {
        public static void Validate(TestHistorySearchRequest request)
        {
            var errors = new List<string>();

            if (request == null)
                throw new ArgumentException("Request is required");

            // -------------------------
            // Date validation
            // -------------------------
            if (request.FilterByDate)
            {
                if (request.RegistrationDateFrom == default)
                    errors.Add("Registration Date From is required");

                if (request.RegistrationDateTo == default)
                    errors.Add("Registration Date To is required");

                if (request.RegistrationDateFrom > request.RegistrationDateTo)
                    errors.Add("Registration Date From cannot be greater than To Date");
            }

            // -------------------------
            // Paging validation
            // -------------------------
            if (request.PageNumber <= 0)
                errors.Add("Page Number must be greater than 0");

            if (request.PageSize <= 0)
                errors.Add("Page Size must be greater than 0");

            if (request.PageSize > 500)
                errors.Add("Page Size cannot exceed 500");

            // -------------------------
            // Optional but safe validations
            // -------------------------
            if (request.Sex < 0 || request.Sex > 2)
                errors.Add("Invalid Sex value");

            if (request.RegistrationCenter < 0)
                errors.Add("Invalid Registration Center");

            if (request.CaseReglocation < 0)
                errors.Add("Invalid Case Registration Location");

            if (request.ConsultantID < 0)
                errors.Add("Invalid Consultant ID");

            if (request.ReferenceID < 0)
                errors.Add("Invalid Reference ID");

            // -------------------------
            // TestStatus (if string like "-1,0,1")
            // -------------------------
            if (!string.IsNullOrEmpty(request.TestStatus))
            {
                var allowed = new[] { "-1", "0", "1", "2", "3" };

                if (!allowed.Contains(request.TestStatus))
                    errors.Add("Invalid TestStatus value");
            }

            // -------------------------
            // Final throw
            // -------------------------
            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}