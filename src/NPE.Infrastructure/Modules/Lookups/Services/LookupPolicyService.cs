using NPE.Core.Common.Enums;
using NPE.Core.Common.Policies;
using NPE.Core.Modules.Lookups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Lookups.Services
{
    public class LookupPolicyService : ILookupPolicyService
    {
        private readonly ApplicationDbContext _context;

        public LookupPolicyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LookupPolicyDTO> GetPolicyAsync(int companyId)
        {
            // TODO:
            // Later load from company settings table

            await Task.CompletedTask;

            return new LookupPolicyDTO
            {
                ConsultantAccessMode = LookupAccessMode.Center,
                ReferenceAccessMode = LookupAccessMode.Center
            };
        }
    }
}
