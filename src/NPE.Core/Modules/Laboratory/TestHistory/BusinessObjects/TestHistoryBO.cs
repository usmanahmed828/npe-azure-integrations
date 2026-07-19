using NPE.Core.Modules.Laboratory.TestHistory.DTOs;
using NPE.Core.Modules.Laboratory.TestHistory.Validators;

namespace NPE.Core.Modules.Laboratory.TestHistory.BusinessObjects
{
    public interface ITestHistoryService
    {
        Task<List<TestHistoryCaseDto>> LoadByPatientCaseForSimpleTestHistoryAsync(
    TestHistorySearchRequest request);
    }

    public class TestHistoryBO
    {
        private readonly ITestHistoryService _service;

        public TestHistoryBO(ITestHistoryService service)
        {
            _service = service;
        }

        public async Task<List<TestHistoryCaseDto>> LoadByPatientCaseForSimpleTestHistoryAsync(
    TestHistorySearchRequest request)
        {
            TestHistoryValidator.Validate(request);

            return await _service.LoadByPatientCaseForSimpleTestHistoryAsync(request);
        }
    }
}