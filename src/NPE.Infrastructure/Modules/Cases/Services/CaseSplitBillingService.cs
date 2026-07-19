using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Infrastructure.Modules.Cases.Entities;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CaseSplitBillingService : ICaseSplitBillingService
    {
        private readonly ApplicationDbContext _context;

        public CaseSplitBillingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(
            long caseId,
            decimal netAmount,
            decimal patientAmount,
            decimal companyAmount,
            CancellationToken cancellationToken = default)
        {
            var entity = new CorporatePaymentFinancial
            {
                CaseId = caseId,

                CaseNetAmount = netAmount,

                CompanyAmount = companyAmount,
                CompanyPaidAmount = 0,
                CompanyBalance = companyAmount,

                PatientAmount = patientAmount,
                PatientPaidAmount = patientAmount,
                PatientBalance = 0
            };

            _context.CorporatePaymentFinancials.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
