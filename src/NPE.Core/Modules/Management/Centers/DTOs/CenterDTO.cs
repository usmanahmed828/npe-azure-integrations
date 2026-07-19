using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Center.Models
{
    public class SaveCenterRequest
    {
        public CenterDTO Center { get; set; } = new();
    }

    public class CenterDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public byte Type { get; set; }

        public bool IsLab { get; set; }
        public bool IsCreditEnabled { get; set; }

        public decimal CreditLimit { get; set; }
        public short CreditDays { get; set; }

        public decimal? Balance { get; set; }

        public int RateTypeId { get; set; }

        public string? Address { get; set; }
        public int? City { get; set; }
        public int? Country { get; set; }

        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }

        public string? ContactPerson { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public string? ContactMobile { get; set; }
        public string? ContactEmail { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }

        public decimal? Rebate { get; set; }
        public decimal? SpecialDiscount { get; set; }
        public decimal? CourierCharges { get; set; }
        public int CompanyId { get; set; }

        public int? DefaultConsultantId { get; set; }
        public int? DefaultReferenceId { get; set; }

        public int MaxLabNumbersPerDay { get; set; }

        public CenterSettingDTO? Setting { get; set; }

        public CenterCreditSummaryDTO? CreditSummary { get; set; }

        public List<ConsultantLookupDTO> Consultants { get; set; } = new();

        public List<ReferenceLookupDTO> References { get; set; } = new();
    }

    public class CenterSettingDTO
    {
        public int CenterId { get; set; }

        public int DestinationLocation { get; set; }

        public int DefaultStatus { get; set; }

        public int? RegionId { get; set; }

        public int? TransportTime { get; set; }

        public bool IsCreditFeatureEnabled { get; set; }

        public decimal? CreditWarningLimit { get; set; }
    }

    public class CenterCreditSummaryDTO
    {
        public decimal TotalAmount { get; set; }

        public decimal TotalUsed { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal CreditUsed { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
