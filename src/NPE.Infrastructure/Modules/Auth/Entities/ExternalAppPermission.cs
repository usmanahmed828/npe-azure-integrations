namespace NPE.Infrastructure.Modules.Auth.Entities
{
    public class ExternalAppPermission
    {
        public int Id { get; set; }
        public string Permission { get; set; } = null!;

        public int ExternalAppID { get; set; }
        public ExternalApp ExternalApp { get; set; } = null!;
    }
}
