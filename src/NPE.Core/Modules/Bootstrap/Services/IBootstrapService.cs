using NPE.Core.Modules.Bootstrap.DTOs;

namespace NPE.Core.Modules.Bootstrap.Services
{
    public interface IBootstrapService
    {
        Task<BootstrapResponseDTO>
            GetAsync();
    }
}