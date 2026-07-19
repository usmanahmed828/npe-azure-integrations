namespace NPE.Core.Modules.Bootstrap.DTOs
{
    public class BootstrapOperationalDTO
    {
        public BootstrapContextDTO
            Context
        { get; set; }
                    = new();

        public BootstrapLookupsDTO
            Lookups
        { get; set; }
                    = new();
    }
}