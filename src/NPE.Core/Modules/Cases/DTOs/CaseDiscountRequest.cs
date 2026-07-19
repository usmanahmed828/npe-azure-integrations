using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Cases.Models
{
    public class CaseDiscountRequest
    {
        public int RegistrationLocation { get; set; }

        public int? ReferenceId { get; set; }

        public int? ConsultantId { get; set; }

        public byte DiscountPercent { get; set; }

        public bool IsManualDiscount { get; set; }
    }
}
