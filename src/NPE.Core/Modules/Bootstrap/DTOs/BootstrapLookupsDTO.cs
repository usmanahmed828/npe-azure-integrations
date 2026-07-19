using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;

namespace NPE.Core.Modules.Bootstrap.DTOs
{
    public class BootstrapLookupsDTO
    {
        public List<CenterLookupDTO>
            RegistrationCenters
        { get; set; }
                    = new();

        public List<CenterLookupDTO>
            DestinationCenters
        { get; set; }
                    = new();

        public List<ConsultantLookupDTO>
            Consultants
        { get; set; }
                    = new();

        public List<ReferenceLookupDTO>
            References
        { get; set; }
                    = new();
    }
}