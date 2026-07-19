using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.Models;
using NPE.Infrastructure.Modules.Management.Entities;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CasePaymentRuleService : ICasePaymentRuleService
    {
        private readonly ApplicationDbContext _context;

        public CasePaymentRuleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CasePaymentRuleResult> EvaluateAsync(
            CasePaymentRuleRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!request.ReferenceId.HasValue)
                return EvaluateSelfPayment(request);

            var reference = await _context.References
                .SingleAsync(r => r.Id == request.ReferenceId.Value, cancellationToken);

            return reference.PaymentMode switch
            {
                0 => EvaluateSelfPayment(request),
                1 => await EvaluateCorporatePaymentAsync(reference, request),
                2 => EvaluateInsurancePayment(request),
                3 => EvaluateInsurancePayment(request),

                _ => new CasePaymentRuleResult
                {
                    IsAllowed = false,
                    ErrorMessage = "Invalid payment mode."
                }
            };
        }

        // ✅ SELF PAYMENT
        private CasePaymentRuleResult EvaluateSelfPayment(
            CasePaymentRuleRequest request)
        {
            if (request.PaidAmount < request.NetAmount)
            {
                return new CasePaymentRuleResult
                {
                    IsAllowed = false,
                    ErrorMessage = "Full payment required.",
                    RequiredMinimumPayment = request.NetAmount
                };
            }

            return new CasePaymentRuleResult
            {
                IsAllowed = true,
                RequiredMinimumPayment = request.NetAmount,
                PatientPortion = request.NetAmount,
                ThirdPartyPortion = 0
            };
        }

        // ✅ CORPORATE PAYMENT + CREDIT LIMIT
        private async Task<CasePaymentRuleResult> EvaluateCorporatePaymentAsync(
            Reference reference,
            CasePaymentRuleRequest request)
        {
            if (reference.CurrentBalance + request.NetAmount > reference.CreditLimit)
            {
                return new CasePaymentRuleResult
                {
                    IsAllowed = false,
                    ErrorMessage = "Credit limit exceeded."
                };
            }

            return new CasePaymentRuleResult
            {
                IsAllowed = true,
                RequiredMinimumPayment = 0,
                PatientPortion = 0,
                ThirdPartyPortion = request.NetAmount
            };
        }

        // ✅ INSURANCE / WELFARE
        private CasePaymentRuleResult EvaluateInsurancePayment(
            CasePaymentRuleRequest request)
        {
            var patientPortion =
                request.NetAmount - request.ThirdPartyCoveredAmount;

            if (request.PaidAmount < patientPortion)
            {
                return new CasePaymentRuleResult
                {
                    IsAllowed = false,
                    ErrorMessage = "Patient portion payment required.",
                    RequiredMinimumPayment = patientPortion
                };
            }

            return new CasePaymentRuleResult
            {
                IsAllowed = true,
                RequiredMinimumPayment = patientPortion,
                PatientPortion = patientPortion,
                ThirdPartyPortion = request.NetAmount - patientPortion
            };
        }
    }
}
