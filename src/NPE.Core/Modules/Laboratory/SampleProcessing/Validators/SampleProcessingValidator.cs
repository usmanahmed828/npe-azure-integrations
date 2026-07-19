using NPE.Core.Common.Validation;
using NPE.Core.Modules.Laboratory.SampleProcessing.DTOs;

namespace NPE.Core.Modules.Laboratory.SampleProcessing.Validators
{
    public static class SampleProcessingValidator
    {
        public static void Validate(SampleProcessingSearchRequest request)
        {
            var errors = new List<string>();

            if (request == null)
                throw new ArgumentException("Request is required");

            if (string.IsNullOrWhiteSpace(request.SampleNumber))
                errors.Add("Sample Number is required");

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}