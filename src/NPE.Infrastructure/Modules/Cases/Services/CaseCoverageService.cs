using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.Models;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CaseCoverageService : ICaseCoverageService
    {
        private readonly ApplicationDbContext _context;

        public CaseCoverageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CaseCoverageResult> ResolveAsync(
            CaseCoverageRequest request,
            CancellationToken cancellationToken = default)
        {
            // ✅ No Reference → No Coverage
            if (!request.ReferenceId.HasValue)
            {
                return NoCoverage(request.NetAmount);
            }

            var reference = await _context.References
                .SingleAsync(r => r.Id == request.ReferenceId.Value, cancellationToken);

            // ✅ Only Insurance / Welfare
            if (reference.PaymentMode != 2 && reference.PaymentMode != 3)
            {
                return NoCoverage(request.NetAmount);
            }

            // ✅ Fetch Coverage Limit (Legacy Assumption)
            // You may later replace with API-based dynamic coverage

            var coverageLimit = await ResolveCoverageLimitAsync(reference.Id);

            if (coverageLimit <= 0)
                return NoCoverage(request.NetAmount);

            // ✅ Option A Logic

            var coverageAmount = Math.Min(request.NetAmount, coverageLimit);

            var patientPayable = request.NetAmount - coverageAmount;

            return new CaseCoverageResult
            {
                CoverageAmount = coverageAmount,
                CompanyPayable = coverageAmount,
                PatientPayable = patientPayable
            };
        }

        private CaseCoverageResult NoCoverage(decimal netAmount)
        {
            return new CaseCoverageResult
            {
                CoverageAmount = 0,
                CompanyPayable = 0,
                PatientPayable = netAmount
            };
        }

        /// <summary>
        /// Placeholder – Replace with Insurance API / Patient Policy Logic
        /// </summary>
        private async Task<decimal> ResolveCoverageLimitAsync(int referenceId)
        {
            // TEMPORARY SAFE DEFAULT
            // Later → Insurance tables / API cache / policy mapping

            return await Task.FromResult(0m);
        }
    }
}
