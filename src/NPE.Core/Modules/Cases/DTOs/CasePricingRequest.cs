using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Cases.Models
{
    public class CasePricingRequest
    {
        public int RegistrationLocation { get; set; }

        public int? ReferenceId { get; set; }

        public int RateTypeId { get; set; }

        public byte DiscountPercent { get; set; }

        public decimal Less { get; set; }

        public decimal? PaidAmount { get; set; }

        public List<TestPricingItem> Tests { get; set; } = new();
    }

    public class TestPricingItem
    {
        public int TestId { get; set; }

        public decimal? ManualRate { get; set; }
    }

    public class CaseFinancials
    {
        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal NetAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal Due { get; set; }
        /// <summary>
        /// Amount covered by Insurance / Welfare
        /// </summary>
        public decimal CoverageAmount { get; set; }

        /// <summary>
        /// Amount patient must pay
        /// </summary>
        public decimal PatientPayable { get; set; }

        /// <summary>
        /// Amount company / insurance must pay
        /// </summary>
        public decimal CompanyPayable { get; set; }
    }

    public class TestRateResultDto
    {
        public int Id { get; set; }

        public decimal Rate { get; set; }
    }
}
