using NPE.Core.Modules.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Tests.BusinessObjects
{
    public interface ITestRateLookupService
    {
        Task<List<TestRateLookupDto>> LoadByReferenceAsync(
            int referenceId,
            short rateTypeId,
            int gender,
            int centerId,
            //int speciesId,
            //int brandId,
            CancellationToken cancellationToken = default);
    }
}
