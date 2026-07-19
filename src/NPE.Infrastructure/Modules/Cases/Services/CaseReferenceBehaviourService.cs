using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.Models;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CaseReferenceBehaviourService : ICaseReferenceBehaviourService
    {
        private readonly ApplicationDbContext _context;

        public CaseReferenceBehaviourService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CaseReferenceBehaviour> ResolveAsync(
            int? referenceId,
            CancellationToken cancellationToken = default)
        {
            // ✅ No Reference → Default Behaviour
            if (!referenceId.HasValue)
            {
                return new CaseReferenceBehaviour
                {
                    IsCouponEnabled = false,
                    IsPrescriptionEnabled = false,
                    IsExtendedSearchEnabled = false,
                    IsLoyaltyCardEnabled = false,
                    IsOutsourceRequestEnabled = false,
                    IsSecondaryReferenceAllowed = false,
                    RequiresAdditionalInfo = false,
                    IsReportAccessAllowed = true,

                    // ✅ Payment Behaviour
                    IsPatientPayment = true,
                    IsCreditPayment = false,
                    IsInsurance = false,
                    IsWelfare = false
                };
            }

            var reference = await _context.References
                .Include(r => r.ReferenceSetting)
                .SingleAsync(r => r.Id == referenceId.Value, cancellationToken);

            var setting = reference.ReferenceSetting;

            var paymentMode = reference.PaymentMode;

            return new CaseReferenceBehaviour
            {
                // ✅ Feature Behaviour
                IsCouponEnabled = setting?.IsCouponEnabled ?? false,
                IsPrescriptionEnabled = setting?.IsPrescriptionEnabled ?? false,
                IsExtendedSearchEnabled = setting?.IsExtendedSearchEnabled ?? false,
                IsLoyaltyCardEnabled = setting?.IsLoyaltyCardEnabled ?? false,
                IsOutsourceRequestEnabled = setting?.IsOutsourceRequestEnabled ?? false,

                IsSecondaryReferenceAllowed = setting?.SecondaryReference ?? false,

                RequiresAdditionalInfo = setting?.AdditionalInfo ?? false,
                AdditionalValidationFields = setting?.AdditionalInfoValidationFields,

                IsReportAccessAllowed = setting?.IsAllowReportAccess ?? true,

                // ✅ Payment Behaviour (CRITICAL)
                IsPatientPayment = paymentMode == 0,
                IsCreditPayment = paymentMode == 1,
                IsInsurance = paymentMode == 2,
                IsWelfare = paymentMode == 3
            };
        }
    }
}
