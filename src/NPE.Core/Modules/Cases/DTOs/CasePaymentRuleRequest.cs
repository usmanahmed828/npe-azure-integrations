using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Cases.Models
{
    public class CasePaymentRuleRequest
    {
        public int? ReferenceId { get; set; }

        public decimal NetAmount { get; set; }

        public decimal PaidAmount { get; set; }

        // Used for Insurance / Welfare
        public decimal ThirdPartyCoveredAmount { get; set; }
    }

    public class CasePaymentRuleResult
    {
        public bool IsAllowed { get; set; }

        public string? ErrorMessage { get; set; }

        public decimal RequiredMinimumPayment { get; set; }

        public decimal PatientPortion { get; set; }

        public decimal ThirdPartyPortion { get; set; }
    }
}
