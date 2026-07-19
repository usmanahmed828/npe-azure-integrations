namespace NPE.Infrastructure.Modules.Auth.Entities
{
    public class ExternalApp
    {
        public int Id { get; set; }
        public string AppId { get; set; } = null!;
        public string SharedSecret { get; set; } = null!;
        public bool IsActive { get; set; }

        public ICollection<ExternalAppPermission> Permissions { get; set; }
            = new List<ExternalAppPermission>();
    }
}
