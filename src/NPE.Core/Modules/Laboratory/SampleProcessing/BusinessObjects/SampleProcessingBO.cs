using NPE.Core.Modules.Laboratory.SampleProcessing.DTOs;
using NPE.Core.Modules.Laboratory.SampleProcessing.Validators;

namespace NPE.Core.Modules.Laboratory.SampleProcessing.BusinessObjects
{
    public interface ISampleProcessingService
    {
        Task<List<SampleProcessingMappedResponse>> SearchAsync(
            SampleProcessingSearchRequest request);
    }

    public class SampleProcessingBO
    {
        private readonly ISampleProcessingService _service;

        public SampleProcessingBO(ISampleProcessingService service)
        {
            _service = service;
        }

        public async Task<List<SampleProcessingMappedResponse>> SearchAsync(
            SampleProcessingSearchRequest request)
        {
            SampleProcessingValidator.Validate(request);

            return await _service.SearchAsync(request);
        }
    }
}