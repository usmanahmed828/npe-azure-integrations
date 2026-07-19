using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.References;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Reference.BusinessObjects;
using NPE.Core.Modules.Management.Reference.DTOs;
using NPE.Infrastructure.Common.UnitOfWork;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/references")]
    public class ReferencesController : BaseCrudController<ReferenceDTO, int>
    {
        private readonly IReferenceService _service;
        private readonly ICurrentContextService _currentContextService;

        public ReferencesController(IReferenceService service, ICurrentContextService currentContextService, IUnitOfWork unitOfWork, IValidator<ReferenceDTO> validator) : base(service, unitOfWork, validator)
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
        //    var reference = await _service.GetByIdAsync(id);

        //    if (reference == null) return NotFound();

        //    return Ok(reference);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _service.GetAllAsync());
        //}

        //[HttpPost]
        //[SwaggerRequestExample(typeof(SaveReferenceRequest), typeof(CreateReferenceExample))]
        //public async Task<IActionResult> Create([FromBody] SaveReferenceRequest request)
        //{
        //    var id = await _service.CreateAsync(request.Reference);
        //    return Ok(id);
        //}

        [HttpPost("create-company-wise")]
        [SwaggerRequestExample(typeof(SaveReferenceRequest), typeof(CreateReferenceExample))]
        public async Task<IActionResult> CreateForCurrentCompanyAsync(SaveReferenceRequest request)
        {
            //var referenceId = await _service.CreateAsync(request.Reference);

            //var context = await _currentContextService.GetAsync();

            //await _service.AssignReferenceToCompanyAsync(context.CompanyId, referenceId);

            //return Ok(referenceId);
            var referenceId = await ExecuteTransactionAsync(async () =>
            {
                var context = await _currentContextService.GetAsync();

                //request.Reference.CompanyId = context.CompanyId;

                var id = await _service.CreateAsync(request.Reference);

                await _service.AssignReferenceToCompanyAsync(context.CompanyId, id);

                return id;
            });

            return Ok(referenceId);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _service.DeleteAsync(id);
        //    return Ok();
        //}
    }
}
