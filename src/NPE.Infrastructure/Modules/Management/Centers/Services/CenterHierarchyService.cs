using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Management.Centers.Services;

namespace NPE.Infrastructure.Modules.Management.Centers.Services
{
    public class CenterHierarchyService : ICenterHierarchyService
    {
        private readonly ApplicationDbContext
            _context;

        public CenterHierarchyService(
            ApplicationDbContext context)
        {
            _context =
                context;
        }

        public async Task<int>
            GetRootCenterIdAsync(
                int centerId)
        {
            var currentCenterMapping =
                await _context.CompanyCenters

                    .AsNoTracking()

                    .FirstOrDefaultAsync(x =>
                        x.CenterId ==
                        centerId);

            // No mapping found
            // Treat itself as root Center
            if (currentCenterMapping == null)
            {
                return centerId;
            }

            var rootCenter =
                await _context.CompanyCenters

                    .AsNoTracking()

                    .FirstOrDefaultAsync(x =>
                        x.CompanyId ==
                        currentCenterMapping.CompanyId
                        &&
                        x.IsRootCenter);

            return rootCenter?.CenterId
                ?? centerId;
        }
    }
}