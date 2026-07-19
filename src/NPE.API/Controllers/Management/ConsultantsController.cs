using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.Consultants;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Consultant.BusinessObjects;
using NPE.Core.Modules.Management.Consultant.DTOs;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Infrastructure.Common.UnitOfWork;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/consultants")]
    public class ConsultantsController : BaseCrudController<ConsultantDto, int>
    {
        private readonly IConsultantService _service;
        private readonly ICurrentContextService _currentContextService;

        public ConsultantsController(IConsultantService service, ICurrentContextService currentContextService, IUnitOfWork unitOfWork, IValidator<ConsultantDto> validator) : base(service, unitOfWork, validator)
        {
            _service = service;
            _currentContextService = currentContextService;
        }

        //[HttpGet("lookup")]
        //public async Task<IActionResult> Lookup()
        //{
        //    return Ok(await _service.GetLookupAsync());
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var result = await _service.GetByIdAsync(id);

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _service.GetAllAsync());
        //}

        //[HttpPost]
        //[SwaggerRequestExample(typeof(SaveConsultantRequest), typeof(CreateConsultantExample))]
        //public async Task<IActionResult> Create([FromBody] SaveConsultantRequest request)
        //{
        //    var id = await _service.CreateAsync(request.Consultant);
        //    return Ok(id);
        //}

        [HttpPost("create-company-wise")]
        [SwaggerRequestExample(typeof(SaveConsultantRequest), typeof(CreateConsultantExample))]
        public async Task<IActionResult> CreateForCurrentCompanyAsync(SaveConsultantRequest request)
        {
            //var consultantId = await _service.CreateAsync(request.Consultant);

            //var context = await _currentContextService.GetAsync();

            //await _service.AssignConsultantToCompanyAsync(context.CompanyId, consultantId);

            //return Ok(consultantId);
            var consultantId = await ExecuteTransactionAsync(async () =>
            {
                var context = await _currentContextService.GetAsync();

                //request.Reference.CompanyId = context.CompanyId;

                var id = await _service.CreateAsync(request.Consultant);

                await _service.AssignConsultantToCompanyAsync(context.CompanyId, id);

                return id;
            });

            return Ok(consultantId);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _service.DeleteAsync(id);
        //    return Ok();
        //}
    }
}
