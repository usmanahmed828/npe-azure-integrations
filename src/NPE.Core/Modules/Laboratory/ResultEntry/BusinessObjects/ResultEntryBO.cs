using NPE.Core.Modules.Laboratory.ResultEntry.DTOs;
using NPE.Core.Modules.Laboratory.ResultEntry.Validators;

namespace NPE.Core.Modules.Laboratory.ResultEntry.BusinessObjects
{
    public interface IResultEntryService
    {
        Task<List<ResultEntryMappedResponse>> LoadPatientAndCaseInfoForTestProcessAsync(
        ResultEntrySearchRequest request);
    }

    public class ResultEntryBO
    {
        private readonly IResultEntryService _service;

        public ResultEntryBO(IResultEntryService service)
        {
            _service = service;
        }

        public async Task<List<ResultEntryMappedResponse>> LoadPatientAndCaseInfoForTestProcessAsync(
            ResultEntrySearchRequest request)
        {
            ResultEntryValidator.Validate(request);

            return await _service.LoadPatientAndCaseInfoForTestProcessAsync(request);
        }
    }
}