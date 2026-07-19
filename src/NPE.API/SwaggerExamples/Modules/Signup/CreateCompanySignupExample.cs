using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Signup.Models;

using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Signup
{
    public class CreateCompanySignupExample
        : IExamplesProvider<CompanySignupRequest>
    {
        public CompanySignupRequest
            GetExamples()
        {
            return new CompanySignupRequest
            {
                CompanyName = "Demo Diagnostic Lab",
                Address = "123 Main Boulevard, Lahore",
                CityId = 1,
                CountryId = 1,
                ContactPerson = "Muhammad Ali",
                Phone = "03001234567",
                Email = "admin@demolab.com",
                Username = "admin",
                Password = "Admin@123",

                Center =
                    new CenterDTO
                    {
                        Name = "Demo Diagnostic Lab",
                        Address = "123 Main Boulevard, Lahore",
                        City = 1,
                        Country = 1,
                        Phone = "03001234567",
                        Email = "admin@demolab.com",
                        ContactPerson = "Muhammad Ali",
                        ContactPhone = "03001234567",
                        Status = true,
                        MaxLabNumbersPerDay = 1000,
                    }
            };
        }
    }
}