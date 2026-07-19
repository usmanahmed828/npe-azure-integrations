namespace NPE.Core.Modules.Auth.DTOs
{
    public sealed class ExternalAppRequest
    {
        public string AppId { get; set; } = null!;
        public string SharedSecret { get; set; } = null!;
    }
    public sealed class ExternalAppModel
    {
        public string AppId { get; set; } = null!;
        public List<string> Permissions { get; set; } = new();
    }
}
