using System;

namespace NPE.Core.Modules.Management.Shared.DTOs
{
    public class CountryDTO
    {
        public int CountryCode { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}
