using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Signup.BusinessObjects
{
    public interface ITenantProvisioningService
    {
        Task ProvisionAsync(
            int companyId,
            CancellationToken cancellationToken = default);
    }
}
