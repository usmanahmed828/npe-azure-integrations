using NPE.Core.Modules.Management.Consultant.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Consultants
{
    public class CreateConsultantExample : IExamplesProvider<SaveConsultantRequest>
    {
        public SaveConsultantRequest GetExamples()
        {
            return new SaveConsultantRequest
            {
                Consultant = new ConsultantDto
                {
                    Code =
                        "CON-001",

                    Name =
                    "Dr Salman Khan",

                    Company =
                    "Salman Medical Associates",

                    Address =
                    "Johar Town, Lahore",

                    City =
                    1,

                    Country =
                    1,

                    Mobile =
                    "03001234567",

                    Phone =
                    "04235123456",

                    Fax =
                    "04235123457",

                    Email =
                    "dr.salman@example.com",

                    Description =
                    "Senior Consultant Physician",

                    Status =
                    true,

                    RegionId =
                    1,

                    Setting = new ConsultantSettingDto
                    {
                        Commission =
                            10,

                        RateTypeId =
                            1,

                        MaxDiscount =
                            20,

                        CommissionCalculationMethod =
                            1,

                        IsTestCountByFlightNumber =
                            false,

                        SecondaryConsultant =
                            false,

                        Speciality =
                            "Internal Medicine"
                    }
                }
            };
        }
    }
}