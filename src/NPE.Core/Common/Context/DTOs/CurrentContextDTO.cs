namespace NPE.Core.Common.Context.DTOs
{
    public class CurrentContextDTO
    {
        public int CompanyId { get; set; }
        public int CenterId { get; set; }
        public int RootCenterId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public bool IsPureSaaS { get; set; }
    }
}