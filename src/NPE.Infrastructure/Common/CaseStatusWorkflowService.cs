using Microsoft.EntityFrameworkCore;
using NPE.Core.Common;
using NPE.Infrastructure.Common.Entities;

namespace NPE.Infrastructure.Common
{
    public class CaseStatusWorkflowService : ICaseStatusWorkflowService
    {
        private readonly ApplicationDbContext _context;

        public CaseStatusWorkflowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetNextStatusesAsync(int currentStatus)
        {
            return await _context.Set<CaseTestStatusWorkFlowEntity>()
                .Where(x => x.CaseStatusID == currentStatus)
                .Select(x => x.NextStatus)
                .ToListAsync();
        }
    }
}