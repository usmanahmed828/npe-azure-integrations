using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Reference.DTOs
{
    public class SaveReferenceRequest
    {
        public ReferenceDTO Reference { get; set; } = new();
    }
    public class ReferenceDTO
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }
        public int? City { get; set; }
        public int? Country { get; set; }

        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }

        public short RateTypeId { get; set; }

        public byte PaymentMode { get; set; }

        public decimal CreditLimit { get; set; }
        public short CreditDays { get; set; }

        public decimal CurrentBalance { get; set; }

        public decimal DefaultDiscount { get; set; }
        public decimal MaxDiscount { get; set; }

        public string? Description { get; set; }

        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactMobile { get; set; }
        public string? ContactEmail { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }

        public ReferenceSettingDTO? Setting { get; set; }
    }
    public class ReferenceSettingDTO
    {
        public bool IsPrescriptionEnabled { get; set; }
        public bool IsCouponEnabled { get; set; }

        public bool? IsExtendedSearchEnabled { get; set; }
        public bool? IsLoyaltyCardEnabled { get; set; }

        public bool IsOutsourceRequestEnabled { get; set; }

        public string? CourierName { get; set; }

        public bool? IsAllowReportAccess { get; set; }
        public bool? SecondaryReference { get; set; }

        public bool? AdditionalInfo { get; set; }
        public string? AdditionalInfoValidationFields { get; set; }

        public string? Settings { get; set; }

        public bool Status { get; set; }
    }
}
