using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Identity;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Infrastructure.Modules.Cases.Entities;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CasePaymentPostingService : ICasePaymentPostingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public CasePaymentPostingService(
            ApplicationDbContext context,
            IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task PostAsync(
            long caseId,
            decimal amount,
            byte method,
            byte type,
            string? referenceNo,
            CancellationToken cancellationToken = default)
        {
            var paymentId = await _identityService.ConsumeAsync(
                0,
                IdentityTypes.Receipt,
                cancellationToken);

            var payment = new CasePayment
            {
                Id = paymentId,
                CaseId = caseId,
                Amount = amount,
                Method = method,
                Type = type,
                Cno = referenceNo,
                CreatedDate = DateTime.Now
            };

            _context.CasePayments.Add(payment);

            // ✅ Bank Tracking
            if (method != 0) // Cash = 0
            {
                var caseEntity = await _context.Cases
                    .SingleAsync(c => c.Id == caseId, cancellationToken);

                caseEntity.BankPaid += amount;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
