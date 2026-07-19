using NPE.Core.Common.Policies;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;

namespace NPE.Core.Modules.Management.Center.Models
{
    public class CenterLookupBundleDTO
    {
        public List<CenterLookupDTO> RegistrationLocations { get; set; } = new();

        public List<CenterLookupDTO> DestinationLocations { get; set; } = new();

        public List<ConsultantLookupDTO> Consultants { get; set; } = new();

        public List<ReferenceLookupDTO> References { get; set; } = new();

        public LookupPolicyDTO Policy { get; set; } = new();
    }

    public class CenterLookupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public bool IsLab { get; set; }

        public int? DestinationLocation { get; set; }

        public int? DefaultStatus { get; set; }

        public bool IsCreditEnabled { get; set; }

        public decimal? CreditLimit { get; set; }

        public List<ConsultantLookupDTO> Consultants { get; set; } = new();

        public List<ReferenceLookupDTO> References { get; set; } = new();
    }
}