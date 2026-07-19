using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.Modules.Patients;
using NPE.Core.Modules.Patients.BusinessObjects;
using NPE.Core.Modules.Patients.Models;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientBO _bo;

        public PatientsController(IPatientBO bo)
        {
            _bo = bo;
        }

        #region CRUD

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
            => Ok(await _bo.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _bo.GetAllAsync());

        /// <summary>Create patient</summary>
        /// <remarks>
        /// Sample request:
        ///
        /// {
        ///   "firstName": "Ali",
        ///   "lastName": "Khan",
        ///   "mobile": "03001234567",
        ///   "city": "Lahore"
        /// }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientDTO dto)
            => Ok(await _bo.CreateAsync(dto));

        [HttpPost("create-return")]
        public async Task<IActionResult> CreateAndReturn([FromBody] PatientDTO dto)
            => Ok(await _bo.CreateAndReturnAsync(dto));

        [HttpPut]
        [SwaggerRequestExample(typeof(PatientDTO), typeof(PatientRegisterExamples))]
        public async Task<IActionResult> Update([FromBody] PatientDTO dto)
        {
            await _bo.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _bo.DeleteAsync(id);
            return Ok();
        }

        #endregion

        //[HttpGet("search")]
        //public async Task<IActionResult> Search(
        //    [FromQuery] string mobile,
        //    [FromQuery] string? name)
        //{
        //    return Ok(await _bo.SearchAsync(
        //        new PatientSearchRequest
        //        {
        //            Mobile = mobile,
        //            Name = name
        //        }));
        //}
        [HttpPost("search")]
        [SwaggerRequestExample(typeof(PatientSearchRequest), typeof(PatientSearchExample))]
        public async Task<IActionResult> Search([FromBody] PatientSearchRequest request)
        {
            var result = await _bo.SearchAsync(request);
            return Ok(result);
        }

        [HttpPost("search-patient-list")]
        public async Task<IActionResult> SearchPatientList([FromBody] CaseSearchParamsDto dto) => Ok(await _bo.SearchPatientListAsync(dto));
    }
}