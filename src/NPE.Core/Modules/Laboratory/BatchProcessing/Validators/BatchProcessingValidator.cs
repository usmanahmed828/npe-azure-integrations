using NPE.Core.Common.Validation;
using NPE.Core.Modules.Laboratory.BatchProcessing.DTOs;

namespace NPE.Core.Modules.Laboratory.BatchProcessing.Validators
{
    public static class BatchProcessingValidator
    {
        public static void Validate(BatchProcessingSearchRequest request)
        {
            var errors = new List<string>();

            if (request == null)
                throw new ArgumentException("Request is required");

            if (string.IsNullOrWhiteSpace(request.CaseNumber) &&
                request.DispatchNo <= 0)
            {
                errors.Add("Either CaseNumber or DispatchNo is required.");
            }

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}