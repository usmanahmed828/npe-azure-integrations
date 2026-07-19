using NPE.Core.Common.Crud;
using NPE.Core.Modules.Management.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Management.Shared.Services
{
    public interface ICityService : ICrudService<CityDTO, int>
    {
        Task<List<CityDTO>> GetByCountryAsync(int countryCode);
    }
}
