using Microsoft.AspNetCore.Mvc;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Core.Modules.Management.Shared.Services;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/management/countries")]
    public class CountriesController : BaseCrudController<CountryDTO, int>
    {
        public CountriesController(
            ICountryService service,
            IUnitOfWork unitOfWork,
            IValidator<CountryDTO> validator) 
            : base(service, unitOfWork, validator)
        {
        }
    }
}
