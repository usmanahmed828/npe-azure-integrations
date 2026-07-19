using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Centers
{
    public class CreateCenterExample : IExamplesProvider<SaveCenterRequest>
    {
        public SaveCenterRequest GetExamples()
        {
            return new SaveCenterRequest
            {
                Center = new CenterDTO
                {
                    Name = "Johar Town Collection Center",
                    Description = "Main collection center for Johar Town region",
                    Type = 0,
                    IsLab = false,
                    IsCreditEnabled = true,
                    CreditLimit = 100000,
                    CreditDays = 30,
                    RateTypeId = 1,
                    Address = "123 Main Boulevard, Johar Town, Lahore",
                    City = 1,
                    Country = 1,
                    Phone = "04235123456",
                    Fax = "04235123457",
                    Email = "johartown@nexusdemo.com",
                    ContactPerson = "Ali Raza",
                    ContactPhone = "03001234567",
                    ContactMobile = "03001234567",
                    ContactEmail = "ali.raza@nexusdemo.com",
                    Status = true,
                    Rebate = 0,
                    SpecialDiscount = 10,
                    CourierCharges = 250,
                    CompanyId = 1,
                    DefaultConsultantId = 1,
                    DefaultReferenceId = 1,
                    MaxLabNumbersPerDay = 1000,

                    Setting = new CenterSettingDTO
                    {
                        DestinationLocation = 1001,
                        DefaultStatus = 1,
                        TransportTime = 60,
                        IsCreditFeatureEnabled = true,
                        CreditWarningLimit = 80000
                    },

                    CreditSummary = new CenterCreditSummaryDTO
                    {
                        TotalAmount = 100000,
                        TotalUsed = 25000,
                        CreditAmount = 100000,
                        CreditUsed = 25000,
                        CurrentBalance = 75000
                    },

                    Consultants = [
                        new ConsultantLookupDTO
                            {
                                Id = 1,
                                Name = "Dr Salman Khan"
                            },

                            new ConsultantLookupDTO
                            {
                                Id = 2,
                                Name = "Dr Ahmed Raza"
                            }
                        ],

                    References =
                        [
                            new ReferenceLookupDTO
                            {
                                Id = 1,
                                Name = "Dr Ahmed Clinic"
                            },

                            new ReferenceLookupDTO
                            {
                                Id = 2,
                                Name = "City Medical Center"
                            }
                        ]
                }
            };
        }
    }
}