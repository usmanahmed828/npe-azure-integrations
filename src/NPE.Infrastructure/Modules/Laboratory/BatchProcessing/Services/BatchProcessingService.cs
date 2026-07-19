using NPE.Core.Modules.Laboratory.BatchProcessing.BusinessObjects;
using NPE.Core.Modules.Laboratory.BatchProcessing.DTOs;
using NPE.Infrastructure.Common.Data;
using static NPE.Core.Modules.Laboratory.BatchProcessing.BusinessObjects.BatchProcessingBO;

namespace NPE.Infrastructure.Modules.Laboratory.BatchProcessing.Services
{
    public class BatchProcessingService : IBatchProcessingService
    {
        private readonly IStoredProcedureExecutor _spExecutor;

        public BatchProcessingService(IStoredProcedureExecutor spExecutor)
        {
            _spExecutor = spExecutor;
        }

        public async Task<List<BatchProcessingMappedResponse>> SearchAsync(
            BatchProcessingSearchRequest request)
        {
            (
                List<BatchProcessingMappedResponse> patients,
                List<BatchProcessingTestDto> tests
            ) = await _spExecutor.ExecuteMultipleAsync<
                    BatchProcessingMappedResponse,
                    BatchProcessingTestDto>(
                "cproc_CaseTestProcess",
                cmd =>
                {
                    DbHelper.AddParam(cmd, "@CaseNumber", request.CaseNumber);
                    DbHelper.AddParam(cmd, "@TestStatus", request.TestStatus);
                    DbHelper.AddParam(cmd, "@DispatchNo", request.DispatchNo);
                });

            var testLookup = tests
                .GroupBy(t => t.CaseID)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var patient in patients)
            {
                patient.Tests = testLookup.ContainsKey(patient.CaseID)
                    ? testLookup[patient.CaseID]
                    : new List<BatchProcessingTestDto>();
            }

            return patients;
        }
    }
}