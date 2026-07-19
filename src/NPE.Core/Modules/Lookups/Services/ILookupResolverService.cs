using NPE.Core.Modules.Lookups.DTOs;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Lookups.Services
{
    public interface ILookupResolverService
    {
        Task<List<ConsultantLookupDTO>> GetConsultantsAsync();

        Task<List<ReferenceLookupDTO>> GetReferencesAsync();

        Task<Dictionary<int, List<ConsultantLookupDTO>>> GetCenterConsultantMapAsync();

        Task<Dictionary<int, List<ReferenceLookupDTO>>> GetCenterReferenceMapAsync();
    }
}
