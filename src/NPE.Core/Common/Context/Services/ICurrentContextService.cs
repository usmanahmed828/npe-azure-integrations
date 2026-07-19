using NPE.Core.Common.Context.DTOs;

namespace NPE.Core.Common.Context.Services
{
    public interface ICurrentContextService
    {
        Task<CurrentContextDTO> GetAsync();
    }
}