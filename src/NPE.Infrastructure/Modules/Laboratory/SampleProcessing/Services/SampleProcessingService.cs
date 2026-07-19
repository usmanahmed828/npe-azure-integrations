using NPE.Core.Common;
using NPE.Core.Modules.Laboratory.SampleProcessing.BusinessObjects;
using NPE.Core.Modules.Laboratory.SampleProcessing.DTOs;
using NPE.Infrastructure.Common.Data;

namespace NPE.Infrastructure.Modules.Laboratory.SampleProcessing.Services
{
    public class SampleProcessingService : ISampleProcessingService
    {
        private readonly IStoredProcedureExecutor _spExecutor;
        //private readonly ITestStatusUpdateService _statusUpdateService;
        //private readonly ICaseStatusWorkflowService _workflowService;

        public SampleProcessingService(IStoredProcedureExecutor spExecutor)//, ITestStatusUpdateService statusUpdateService, ICaseStatusWorkflowService workflowService)
        {
            _spExecutor = spExecutor;
            //_statusUpdateService = statusUpdateService;
            //_workflowService = workflowService;
        }

        public async Task<List<SampleProcessingMappedResponse>> SearchAsync(
            SampleProcessingSearchRequest request)
        {
            (
                List<SampleProcessingMappedResponse> patients,
                List<SampleProcessingTestDto> tests
            ) = await _spExecutor.ExecuteMultipleAsync<
                    SampleProcessingMappedResponse,
                    SampleProcessingTestDto>(
                "cproc_SampleProcessing",
                cmd =>
                {
                    DbHelper.AddParam(cmd, "@SampleNumber", request.SampleNumber);
                    DbHelper.AddParam(cmd, "@StatusID", request.StatusID);
                });

            var testLookup = tests
                .GroupBy(t => t.CaseID)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var patient in patients)
            {
                patient.Tests = testLookup.ContainsKey(patient.CaseID)
                    ? testLookup[patient.CaseID]
                    : new List<SampleProcessingTestDto>();

                //var currentStatus = patient.Tests.FirstOrDefault()?.TestStatus;

                //if (currentStatus != null)
                //{
                //    var nextStatuses = await _workflowService.GetNextStatusesAsync(currentStatus.Value);

                //    patient.NextStatus1 = nextStatuses.ElementAtOrDefault(0);
                //    patient.NextStatus2 = nextStatuses.ElementAtOrDefault(1);
                //    patient.NextStatus3 = nextStatuses.ElementAtOrDefault(2);
                //    patient.NextStatus4 = nextStatuses.ElementAtOrDefault(3);
                //}

            }

            //await _statusUpdateService.UpdateCaseTestStatusAsync(
            //        tests.Select(t => t.ID),
            //        4,
            //        "Administrator");

            return patients;
        }
    }
}