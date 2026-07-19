using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Tests.BusinessObjects;
using NPE.Core.Modules.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Tests.Services
{
    public class TestRateLookupService : ITestRateLookupService
    {
        private readonly ApplicationDbContext _context;

        public TestRateLookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TestRateLookupDto>> LoadByReferenceAsync(
            int referenceId,
            short rateTypeId,
            int gender,
            int centerId,
            //int speciesId,
            //int brandId,
            CancellationToken cancellationToken = default)
        {
            var parameters = new[]
            {
                new SqlParameter("@ReferenceID", referenceId),
                new SqlParameter("@RateTypeID", rateTypeId),
                new SqlParameter("@TestFor", gender),
                new SqlParameter("@CenterId", centerId),
                //new SqlParameter("@SpeciesID", speciesId),
                //new SqlParameter("@BrandID", brandId)
            };

            var results = await _context.TestRateLookupDtos
                .FromSqlRaw(
                    "EXEC dbo.cproc_TestRatesByReferenceWithGenderFilter " +
                    "@ReferenceID,@RateTypeID,@TestFor,@CenterId",
                    parameters)
                .ToListAsync(cancellationToken);

            return results;
        }
    }
}
