using NPE.Core.Modules.Cases.Models;

namespace NPE.Core.Modules.Cases.BusinessObjects
{
    public interface ICaseCoverageService
    {
        Task<CaseCoverageResult> ResolveAsync(
            CaseCoverageRequest request,
            CancellationToken cancellationToken = default);
    }

    public interface ICaseDiscountService
    {
        Task<byte> ResolveDiscountAsync(
            CaseDiscountRequest request,
            CancellationToken cancellationToken = default);

        Task ValidateDiscountAsync(
            CaseDiscountRequest request,
            CancellationToken cancellationToken = default);
    }

    //public interface ICasePaymentRuleService
    //{
    //    Task<CasePaymentRuleResult> EvaluateAsync(
    //        CasePaymentRuleRequest request,
    //        CancellationToken cancellationToken = default);
    //}

    //public interface ICasePaymentPostingService
    //{
    //    Task PostAsync(
    //        long caseId,
    //        decimal amount,
    //        byte method,
    //        byte type,
    //        string? referenceNo,
    //        CancellationToken cancellationToken = default);
    //}

    //public interface ICaseSplitBillingService
    //{
    //    Task CreateAsync(
    //        long caseId,
    //        decimal netAmount,
    //        decimal patientAmount,
    //        decimal companyAmount,
    //        CancellationToken cancellationToken = default);
    //}

    public interface ICasePricingService
    {
        Task<CaseFinancials> CalculateAsync(
            CasePricingRequest request,
            CancellationToken cancellationToken = default);
    }

    public interface ICaseReferenceBehaviourService
    {
        Task<CaseReferenceBehaviour> ResolveAsync(
            int? referenceId,
            CancellationToken cancellationToken = default);
    }
}
