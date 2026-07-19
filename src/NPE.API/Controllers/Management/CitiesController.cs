using Microsoft.AspNetCore.Mvc;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Core.Modules.Management.Shared.Services;
using System.Threading.Tasks;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/management/cities")]
    public class CitiesController : BaseCrudController<CityDTO, int>
    {
        private readonly ICityService _cityService;

        public CitiesController(
            ICityService service,
            IUnitOfWork unitOfWork,
            IValidator<CityDTO> validator) 
            : base(service, unitOfWork, validator)
        {
            _cityService = service;
        }

        [HttpGet("by-country/{countryCode}")]
        public async Task<IActionResult> GetByCountry(int countryCode)
        {
            var result = await _cityService.GetByCountryAsync(countryCode);
            return Ok(result);
        }
    }
}
