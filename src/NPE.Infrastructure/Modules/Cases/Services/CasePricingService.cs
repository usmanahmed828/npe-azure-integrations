using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.Models;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CasePricingService : ICasePricingService
    {
        private readonly ApplicationDbContext _context;

        public CasePricingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CaseFinancials> CalculateAsync(
            CasePricingRequest request,
            CancellationToken cancellationToken = default)
        {
            decimal total = 0;

            foreach (var test in request.Tests)
            {
                decimal rate;

                if (test.ManualRate.HasValue)
                {
                    rate = test.ManualRate.Value;
                }
                else
                {
                    rate = await ResolveRateFromProcedureAsync(
                        request,
                        test.TestId,
                        cancellationToken);
                }

                total += rate;
            }

            var discountAmount = total * request.DiscountPercent / 100m;

            var net = total - discountAmount - request.Less;

            var paid = request.PaidAmount ?? 0m;

            var due = Math.Max(0m, net - paid);

            return new CaseFinancials
            {
                TotalAmount = total,
                DiscountAmount = discountAmount,
                NetAmount = net,
                Due = due
            };
        }

        private async Task<decimal> ResolveRateFromProcedureAsync(
            CasePricingRequest request,
            int testId,
            CancellationToken ct)
        {
            var result = await _context.TestRateResults
                .FromSqlRaw(@"
                    EXEC dbo.cproc_TestRatesByReferenceAndTest
                        @ReferenceID,
                        @RateTypeID,
                        @TestFor,
                        @CenterId,
                        @TestID",

                    new SqlParameter("@ReferenceID",
                        request.ReferenceId ?? (object)DBNull.Value),

                    new SqlParameter("@RateTypeID", request.RateTypeId),

                    new SqlParameter("@TestFor", 0),   // Future gender logic
                    new SqlParameter("@CenterId", request.RegistrationLocation),

                    new SqlParameter("@TestID", testId))
                .AsNoTracking()
                .FirstOrDefaultAsync(ct);

            if (result == null)
                throw new InvalidOperationException(
                    $"Rate not found for Test={testId}");

            return result.Rate;
        }
    }
}
