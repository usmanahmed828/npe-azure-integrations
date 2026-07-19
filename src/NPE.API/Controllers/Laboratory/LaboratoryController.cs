using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.Modules.TestHistory;
using NPE.Core.Modules.Laboratory.ResultEntry.BusinessObjects;
using NPE.Core.Modules.Laboratory.ResultEntry.DTOs;
using NPE.Core.Modules.Laboratory.TestHistory.BusinessObjects;
using NPE.Core.Modules.Laboratory.TestHistory.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers.Laboratory
{
    [ApiController]
    [Route("api/laboratory")]
    public class LaboratoryController : ControllerBase
    {
        private readonly ResultEntryBO _resultEntryBO;
        private readonly ITestHistoryService _testHistoryService;

        public LaboratoryController(
            ResultEntryBO resultEntryBO,
            ITestHistoryService testHistoryService)
        {
            _resultEntryBO = resultEntryBO;
            _testHistoryService = testHistoryService;
        }

        #region Result Entry

        [HttpPost("result-entry/search-tests")]
        public async Task<IActionResult> SearchTests(
            [FromBody] ResultEntrySearchRequest request)
        {
            var result = await _resultEntryBO
                .LoadPatientAndCaseInfoForTestProcessAsync(request);

            return Ok(result);
        }

        #endregion

        #region Test History

        [HttpPost("test-history/search")]
        [SwaggerRequestExample(typeof(TestHistorySearchRequest), typeof(SearchTestHistoryExample))]
        public async Task<IActionResult> TestHistorySearch(
            [FromBody] TestHistorySearchRequest request)
        {
            try
            {
                var result = await _testHistoryService.LoadByPatientCaseForSimpleTestHistoryAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    errors = new[] { "Something went wrong" },
                    details = ex.Message
                });
            }
        }

        #endregion
    }
}