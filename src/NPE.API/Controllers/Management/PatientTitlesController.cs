using Microsoft.AspNetCore.Mvc;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Shared.DTOs;
using NPE.Core.Modules.Management.Shared.Services;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/management/patient-titles")]
    public class PatientTitlesController : BaseCrudController<PatientTitleDTO, int>
    {
        public PatientTitlesController(IPatientTitleService service, IUnitOfWork unitOfWork, IValidator<PatientTitleDTO> validator) : base(service, unitOfWork, validator)
        {

        }
    }
}
