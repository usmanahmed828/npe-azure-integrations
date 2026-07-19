using NPE.Core.Modules.Patients.Models;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Modules.Patients
{
    public class PatientRegisterExamples : IExamplesProvider<PatientDTO>
    {
        public PatientDTO GetExamples()
        {
            return new PatientDTO
            {
                FirstName = "Muhammad",
                LastName = "Waqas",
                Fhname = "Mehmood",

                Sex = 1,

                DateOfBirth =
                    new DateTime(
                        1995,
                        10,
                        15),

                MaritalStatus = 0,

                BloodGroup = "B+",

                Nic = "35202-1234567-1",

                Phone = "",

                Mobile = "03006709010",

                Fax = "",

                Email = "waqas@example.com",

                Address =
                    "Johar Town",

                City =
                    "Lahore",

                Country =
                    "Pakistan",

                DateRegistered =
                    DateTime.UtcNow,

                CreatedBy =
                    "Admin",

                Status = true,

                Location = 1001,

                Cabgno = "",

                MedicalRecordNo = "",

                PatientDetail = null,

                PatientSetting = null,

                PatientCorporateInfo = null
            };
        }
    }
}