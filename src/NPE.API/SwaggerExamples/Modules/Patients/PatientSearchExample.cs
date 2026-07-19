using NPE.Core.Modules.Patients.Models;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Modules.Patients
{
    public class PatientSearchExample : IExamplesProvider<PatientSearchRequest>
    {
        public PatientSearchRequest GetExamples()
        {
            return new PatientSearchRequest
            {
                Phone = "03006709010",
                FullName = "TEST",
                Location = 1001,
                PageNo = 1,
                PageSize = 25
            };
        }
    }
}
