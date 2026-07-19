using System;

namespace NPE.Core.Modules.Management.Shared.DTOs
{
    public class CityDTO
    {
        public int CityCode { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int CountryCode { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}
