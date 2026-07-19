using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.Models;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CaseDiscountService : ICaseDiscountService
    {
        private readonly ApplicationDbContext _context;

        public CaseDiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<byte> ResolveDiscountAsync(
            CaseDiscountRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request.IsManualDiscount)
                return request.DiscountPercent;

            // ✅ Reference Discount Priority
            if (request.ReferenceId.HasValue)
            {
                var discount = await _context.References
                    .Where(r => r.Id == request.ReferenceId.Value)
                    .Select(r => r.DefaultDiscount)
                    .SingleAsync(cancellationToken);

                return Convert.ToByte(Math.Round(discount, 0));
            }

            return 0;
        }

        public async Task ValidateDiscountAsync(
            CaseDiscountRequest request,
            CancellationToken cancellationToken = default)
        {
            decimal maxDiscount = 0;

            // ✅ Reference Max Discount (PRIMARY RULE)
            if (request.ReferenceId.HasValue)
            {
                maxDiscount = await _context.References
                    .Where(r => r.Id == request.ReferenceId.Value)
                    .Select(r => r.MaxDiscount)
                    .SingleAsync(cancellationToken);
            }
            // ✅ Consultant Discount Limit (SECONDARY RULE)
            else if (request.ConsultantId.HasValue)
            {
                var consultantMax = await _context.ConsultantSettings
                    .Where(c => c.ConsultantId == request.ConsultantId.Value)
                    .Select(c => c.MaxDiscount)
                    .SingleOrDefaultAsync(cancellationToken);

                if (consultantMax.HasValue)
                    maxDiscount = consultantMax.Value;
            }

            if (maxDiscount > 0 && request.DiscountPercent > maxDiscount)
                throw new InvalidOperationException(
                    $"Discount exceeds allowed limit ({maxDiscount}%).");
        }
    }
}
