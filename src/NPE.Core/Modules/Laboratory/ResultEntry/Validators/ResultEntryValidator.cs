using NPE.Core.Common.Validation;
using NPE.Core.Modules.Laboratory.ResultEntry.DTOs;

namespace NPE.Core.Modules.Laboratory.ResultEntry.Validators
{
    public static class ResultEntryValidator
    {
        public static void Validate(ResultEntrySearchRequest request)
        {
            var errors = new List<string>();

            if (request == null)
                throw new ArgumentException("Request is required");

            if (request.FilterByDate)
            {
                if (request.FromDate == default)
                    errors.Add("From Date is required");

                if (request.ToDate == default)
                    errors.Add("To Date is required");

                if (request.FromDate > request.ToDate)
                    errors.Add("From Date cannot be greater than To Date");
            }

            if (request.RegistrationLocation < 0)
                errors.Add("Invalid Registration Location");

            if (request.ConductedAt < 0)
                errors.Add("Invalid ConductedAt");

            if (request.Consultant < 0)
                errors.Add("Invalid Consultant");

            if (request.Reference < 0)
                errors.Add("Invalid Reference");
            if (request.IsDelayed != "-1" && request.IsDelayed != "0" && request.IsDelayed != "1")
            {
                errors.Add("IsDelayed must be -1, 0, or 1");
            }

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}