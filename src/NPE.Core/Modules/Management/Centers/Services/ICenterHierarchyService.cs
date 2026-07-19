using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Centers.Services
{
    public interface ICenterHierarchyService
    {
        Task<int>
            GetRootCenterIdAsync(
                int centerId);
    }
}
