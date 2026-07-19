using NPE.Core.Modules.Laboratory.BatchProcessing.DTOs;
using NPE.Core.Modules.Laboratory.BatchProcessing.Validators;

namespace NPE.Core.Modules.Laboratory.BatchProcessing.BusinessObjects
{
    public class BatchProcessingBO
    {
        public interface IBatchProcessingService
        {
            Task<List<BatchProcessingMappedResponse>> SearchAsync(
                BatchProcessingSearchRequest request);
        }

        private readonly IBatchProcessingService _service;

        public BatchProcessingBO(IBatchProcessingService service)
        {
            _service = service;
        }

        public async Task<List<BatchProcessingMappedResponse>> SearchAsync(
            BatchProcessingSearchRequest request)
        {
            BatchProcessingValidator.Validate(request);

            return await _service.SearchAsync(request);
        }
    }
}