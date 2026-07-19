using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Tests.Models
{
    public class TestRateLookupDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Synonyms { get; set; }

        public decimal Rate { get; set; }

        public string? ReportDate { get; set; }

        public int? TransportTime { get; set; }

        public string? TestDate { get; set; }

        public byte? TestGender { get; set; }
    }
}
