using NPE.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Common.Policies
{
    public class LookupPolicyDTO
    {
        public LookupAccessMode ConsultantAccessMode { get; set; }

        public LookupAccessMode ReferenceAccessMode { get; set; }
    }
}
