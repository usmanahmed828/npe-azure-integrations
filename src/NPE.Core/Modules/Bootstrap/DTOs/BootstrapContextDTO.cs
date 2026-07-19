namespace NPE.Core.Modules.Bootstrap.DTOs
{
    public class BootstrapContextDTO
    {
        public int CompanyId
        { get; set; }

        public int CenterId
        { get; set; }

        public int RootCenterId
        { get; set; }

        public int UserId
        { get; set; }

        public string UserName
        { get; set; } = "";

        public bool IsPureSaaS
        { get; set; }
    }
}