using NPE.Core.Modules.Management.Reference.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.References
{
    public class CreateReferenceExample : IExamplesProvider<SaveReferenceRequest>
    {
        public SaveReferenceRequest GetExamples()
        {
            return new SaveReferenceRequest
            {
                Reference = new ReferenceDTO
                {
                    Code =
                            "REF-001",

                    Name =
                            "Dr Ahmed Clinic",

                    Address =
                            "Johar Town, Lahore",

                    City =
                            1,

                    Country =
                            1,

                    Phone =
                            "04235123456",

                    Fax =
                            "04235123457",

                    Email =
                            "contact@drahmedclinic.com",

                    RateTypeId =
                            1,

                    PaymentMode =
                            1,

                    CreditLimit =
                            50000,

                    CreditDays =
                            30,

                    CurrentBalance =
                            0,

                    DefaultDiscount =
                            10,

                    MaxDiscount =
                            20,

                    Description =
                            "Primary referral clinic",

                    ContactPerson =
                            "Dr Ahmed",

                    ContactPhone =
                            "04235123456",

                    ContactMobile =
                            "03001234567",

                    ContactEmail =
                            "dr.ahmed@clinic.com",

                    Status =
                            true,

                    Setting =
                            new ReferenceSettingDTO
                            {
                                IsPrescriptionEnabled =
                                    true,

                                IsCouponEnabled =
                                    true,

                                IsExtendedSearchEnabled =
                                    true,

                                IsLoyaltyCardEnabled =
                                    false,

                                IsOutsourceRequestEnabled =
                                    false,

                                CourierName =
                                    "TCS",

                                IsAllowReportAccess =
                                    true,

                                SecondaryReference =
                                    false,

                                AdditionalInfo =
                                    false,

                                AdditionalInfoValidationFields =
                                    null,

                                Settings =
                                    null,

                                Status =
                                    true
                            }
                }
            };
        }
    }
}