using NPE.Core.Modules.Users.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Users
{
    public class CreateAdminUserExample
        : IExamplesProvider<CreateAdminUserRequest>
    {
        public CreateAdminUserRequest
            GetExamples()
        {
            return new CreateAdminUserRequest
            {
                CompanyId =
                    1,

                RootCenterId =
                    1001,

                DefaultReferenceId =
                    1,

                Username =
                    "admin",

                Password =
                    "Admin@123",

                FullName =
                    "Muhammad Ali",

                Email =
                    "admin@demolab.com",

                Phone =
                    "03001234567"
            };
        }
    }
}