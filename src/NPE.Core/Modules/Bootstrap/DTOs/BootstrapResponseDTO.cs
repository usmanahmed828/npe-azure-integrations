namespace NPE.Core.Modules.Bootstrap.DTOs
{
    public class BootstrapResponseDTO
    {
        public BootstrapSecurityDTO
            Security
        { get; set; }
                    = new();

        public BootstrapOperationalDTO
            Operational
        { get; set; }
                    = new();
    }
}