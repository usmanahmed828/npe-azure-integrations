using NPE.Core.Modules.Cases.DTOs;
using NPE.Core.Modules.Cases.Models;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.SwaggerExamples.Cases;

public class CreateCaseExample : IExamplesProvider<CaseDTO>
{
    public CaseDTO GetExamples()
    {
        return new CaseDTO
        {
            PatientId = 2,
            RegistrationLocation = 1001,
            DestinationLocation = 1001,
            RegistrationDate = DateTime.Now,
            ReportingDate = DateTime.Now.AddDays(1),

            ReferenceId = 1,
            ReferenceName = "Dr Ahmed Referral",

            ConsultantId = 1,
            ConsultantName = "Dr Salman Consultant",

            TotalAmount = 1000,
            Discount = 10,
            Less = 100,
            NetAmount = 800,
            PaidAmount = 600,
            BankPaid = 0,
            Due = 200,
            DueReceived = 0,
            BankDueReceived = 0,

            Comments = "Routine blood work and diabetes profile",

            Completed = false,

            CreatedBy = "admin",
            CreatedDate = DateTime.Now,
            ModifiedBy = "admin",
            ModifiedDate = DateTime.Now,

            Status = true,
            AlertSent = false,
            WithoutHistory = false,

            Details = new()
        {
            new CaseDetailDTO
            {
                TestId = 5,
                TestName = "Urine C/E",
                Rate = 500,
                TestStatus = 1,
                ConductedAt = 1001,
                ReportingDate = DateTime.Now.AddDays(1),
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "admin",
                ModifiedDate = DateTime.Now,
                Status = true,
                TemplateId = 4,
                Comments = "Urgent sample",
                IsDelayed = false,
                ConductedBy = "Lab Tech Ali",
                ApprovedBy = "",
                ExternalSystemDetailId = "",
                SyncDateTime = DateTime.Now,

                Instrument = new CaseDetailInstrumentDTO
                {
                    InstrumentId = 5,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "admin",
                    ModifiedDate = DateTime.Now
                },

                Outsource = null
            },

            new CaseDetailDTO
            {
                TestId = 6,
                TestName = "Heamoglobin (Hb)",
                Rate = 500,
                TestStatus = 1,
                ConductedAt = 1001,
                ReportingDate = DateTime.Now.AddDays(1),
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "admin",
                ModifiedDate = DateTime.Now,
                Status = true,
                TemplateId = 4,
                Comments = "",
                IsDelayed = false,
                ConductedBy = "Lab Tech Sana",
                ApprovedBy = "",
                ExternalSystemDetailId = "",
                SyncDateTime = DateTime.Now,

                Instrument = null,
                Outsource = null
            }
        },

            Payments = new()
        {
            new CasePaymentDTO
            {
                Method = 0,
                Type = 0,
                Amount = 600,
                Cno = "",
                Description = "Paid Amount of Case",
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "admin",
                ModifiedDate = DateTime.Now,
                CenterId = 1001
            }
        },

            AdditionalSetting = new CaseAdditionalSettingDTO
            {
                SecondReferenceId = 2,
                SecondReferenceName = "Dr Hassan",
                SecondConsultantId = 4,
                SecondConsultantName = "Dr Bilal",
                MedicalRecordNo = "MRN-22015",
                SampleReceivedFrom = "Reception",
                SampleReceivedBy = "Nurse Maria",
                Ponumber = "PO-5567",
                CaseSettings = "Priority"
            },

            ClinicalFindings = new()
        {
            new CaseClinicalDetailDTO
            {
                ClinicalDetailCode = "CF01",
                Name = "Diabetes",
                Description = "Known diabetic patient, fasting sample",
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "admin",
                ModifiedDate = DateTime.Now
            }
        },

            CaseInfo = new CaseInfoDTO
            {
                Server = "NEXUS-SRV01",
                ClientIp = "192.168.1.25",
                ClientName = "FrontDesk-PC",
                UserId = 1
            },

            PaymentOnline = null,

            Remarks = new()
        {
            new CaseRemarkDTO
            {
                RemarkId = 4444,
                Description = "Urgent Result Delivery",
                Type = true,
                Rate = 0,
                CreatedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedBy = "admin",
                ModifiedDate = DateTime.Now,
                Status = true
            }
        },

            Setting = new CaseSettingDTO
            {
                IsCompleted = false,
                IsAlertSent = false,
                IsEmailSent = false,
                BborderNumber = "",
                Bbtype = "",
                Status = true,
                Bbdin = "",
                Qrstring = "",
                //Qrimage = ""
            },

            CorporatePaymentFinancial = null
        };
    }
}