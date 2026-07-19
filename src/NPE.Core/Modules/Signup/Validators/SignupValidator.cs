using NPE.Core.Common.Validation;
using NPE.Core.Modules.Signup.Models;

namespace NPE.Core.Modules.Signup.Validators
{
    public static class SignupValidator
    {
        public static void Validate(CompanySignupRequest request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CompanyName))
                errors.Add("Company Name is required");

            if (string.IsNullOrWhiteSpace(request.Address))
                errors.Add("Address is required");

            if (request.CityId <= 0)
                errors.Add("Invalid City");

            if (request.CountryId <= 0)
                errors.Add("Invalid Country");

            if (string.IsNullOrWhiteSpace(request.Phone))
                errors.Add("Phone is required");

            if (!string.IsNullOrWhiteSpace(request.Email) &&
                !request.Email.Contains("@"))
                errors.Add("Invalid email");

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}