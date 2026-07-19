using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.Centers;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Management.Center.Models;
using NPE.Core.Modules.Management.Centers.Services;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers.Management
{
    [ApiController]
    [Route("api/centers")]
    public class CentersController : BaseCrudController<CenterDTO, int>
    {
        private readonly ICenterService _service;
        private readonly ICurrentContextService _currentContextService;
        //private readonly IUnitOfWork _unitOfWork;

        public CentersController(ICenterService service, ICurrentContextService currentContextService, IUnitOfWork unitOfWork, IValidator<CenterDTO> validator) : base(service, unitOfWork, validator)
        {
            _service = service;
            _currentContextService = currentContextService;
            //_unitOfWork = unitOfWork;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup()
        {
            return Ok(await _service.GetLookupAsync());
        }
        //[HttpGet("{id}")]
        //public override async Task<IActionResult> GetById(int id)
        //{
        //    var center = await _service.GetByIdAsync(id);

        //    if (center == null) return NotFound();

        //    return Ok(center);
        //}

        //[HttpGet]
        //public override async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _service.GetAllAsync());
        //}

        //[HttpPost]
        //[SwaggerRequestExample(typeof(SaveCenterRequest), typeof(CreateCenterExample))]
        //public async Task<IActionResult> Create([FromBody] SaveCenterRequest request)
        //{
        //    //var id = await _service.CreateAsync(request.Center);

        //    //return Ok(id);

        //    var centerId = await ExecuteTransactionAsync(async () => await _service.CreateAsync(request.Center));

        //    return Ok(centerId);
        //}

        [HttpPost("create-company-wise")]
        [SwaggerRequestExample(typeof(SaveCenterRequest), typeof(CreateCenterExample))]
        public async Task<IActionResult> CreateForCurrentCompanyAsync(SaveCenterRequest request)
        {
            //var context = await _currentContextService.GetAsync();

            //request.Center.CompanyId = context.CompanyId;

            //var centerId = await _service.CreateAsync(request.Center);

            //await _service.AssignCenterToCompanyAsync(context.CompanyId, centerId, false);

            //return Ok(centerId);

            var centerId = await ExecuteTransactionAsync(async () =>
                    {
                        var context = await _currentContextService.GetAsync();

                        request.Center.CompanyId = context.CompanyId;

                        var id = await _service.CreateAsync(request.Center);

                        await _service.AssignCenterToCompanyAsync(context.CompanyId, id, false);

                        return id;
                    });

            return Ok(centerId);
        }

        //[HttpDelete("{id}")]
        //public override async Task<IActionResult> Delete(int id)
        //{
        //    await _service.DeleteAsync(id);
        //    return Ok();
        //}
    }
}
