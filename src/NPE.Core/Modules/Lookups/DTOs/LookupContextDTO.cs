namespace NPE.Core.Modules.Lookups.DTOs
{
    public class LookupContextDTO
    {
        public int CompanyId
        { get; set; }

        // Current logged-in Center
        public int CenterId
        { get; set; }

        // Main operational/root Center
        public int RootCenterId
        { get; set; }

        public int UserId
        { get; set; }
    }
}