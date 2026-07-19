using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Cases.Models
{
    public class CaseCoverageRequest
    {
        public int? ReferenceId { get; set; }

        public decimal NetAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
    }

    public class CaseCoverageResult
    {
        public decimal CoverageAmount { get; set; }

        public decimal PatientPayable { get; set; }

        public decimal CompanyPayable { get; set; }
        public decimal PatientAmount { get; set; }
    }
}
