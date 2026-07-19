using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Cases.Models
{
    public class CaseReferenceBehaviour
    {
        public bool IsCouponEnabled { get; set; }

        public bool IsPrescriptionEnabled { get; set; }

        public bool IsExtendedSearchEnabled { get; set; }

        public bool IsLoyaltyCardEnabled { get; set; }

        public bool IsOutsourceRequestEnabled { get; set; }

        public bool IsSecondaryReferenceAllowed { get; set; }

        public bool RequiresAdditionalInfo { get; set; }

        public string? AdditionalValidationFields { get; set; }

        public bool IsReportAccessAllowed { get; set; }

        public bool IsCreditPayment { get; set; }
        public bool IsInsurance { get; set; }

        public bool IsWelfare { get; set; }

        public bool IsPatientPayment { get; set; }
    }
}
