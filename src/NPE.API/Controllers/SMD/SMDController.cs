using Microsoft.AspNetCore.Mvc;
using NPE.Core.Modules.Laboratory.BatchProcessing.BusinessObjects;
using NPE.Core.Modules.Laboratory.BatchProcessing.DTOs;
using NPE.Core.Modules.Laboratory.SampleProcessing.BusinessObjects;
using NPE.Core.Modules.Laboratory.SampleProcessing.DTOs;

namespace NPE.API.Controllers.SMD
{
    [ApiController]
    [Route("api/smd")]
    public class SMDController : ControllerBase
    {
        private readonly BatchProcessingBO _batchBO;
        private readonly SampleProcessingBO _sampleBO;

        public SMDController(
            BatchProcessingBO batchBO,
            SampleProcessingBO sampleBO)
        {
            _batchBO = batchBO;
            _sampleBO = sampleBO;
        }

        #region Batch Processing

        [HttpPost("batch/search")]
        public async Task<IActionResult> BatchSearch(
            [FromBody] BatchProcessingSearchRequest request)
        {
            try
            {
                var result = await _batchBO.SearchAsync(request);
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

        #region Sample Processing

        [HttpPost("sample/search")]
        public async Task<IActionResult> SampleSearch(
            [FromBody] SampleProcessingSearchRequest request)
        {
            var result = await _sampleBO.SearchAsync(request);
            return Ok(result);
        }

        #endregion
    }
}