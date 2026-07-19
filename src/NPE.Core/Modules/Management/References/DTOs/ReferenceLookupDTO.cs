using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Reference.DTOs
{
    public class ReferenceLookupDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int? RateTypeId { get; set; }

        public decimal? MaxDiscount { get; set; }

        public decimal? DefaultDiscount { get; set; }
        public int PaymentMode { get; set; }
    }
}
