using NPE.Core.Modules.Management.Reference.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Consultant.DTOs
{
    public class SaveConsultantRequest
    {
        public ConsultantDto Consultant { get; set; } = new();
    }
    public class ConsultantDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        public string? Address { get; set; }

        public int? City { get; set; }
        public int? Country { get; set; }

        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public int? RegionId { get; set; }

        public ConsultantSettingDto? Setting { get; set; }
    }

    public class ConsultantSettingDto
    {
        public int ConsultantId { get; set; }

        public decimal Commission { get; set; }
        public int? RateTypeId { get; set; }

        public decimal? MaxDiscount { get; set; }

        public byte? CommissionCalculationMethod { get; set; }

        public bool? IsTestCountByFlightNumber { get; set; }
        public bool? SecondaryConsultant { get; set; }

        public string? Speciality { get; set; }
    }
}
