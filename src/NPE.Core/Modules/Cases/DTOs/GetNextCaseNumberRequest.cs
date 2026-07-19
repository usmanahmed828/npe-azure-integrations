using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Cases.DTOs
{
    public class GetNextCaseNumberRequest
    {
        public int Location { get; set; }

        public DateTime CaseRegDate { get; set; }
    }
}
