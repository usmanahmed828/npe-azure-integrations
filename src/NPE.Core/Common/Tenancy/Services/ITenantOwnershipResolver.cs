namespace NPE.Core.Common.Tenancy.Services
{
    public interface ITenantOwnershipResolver
    {
        bool IsHybridLegacy();

        bool IsPureSaaS();
    }
}