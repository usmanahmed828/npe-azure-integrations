using NPE.Core.Common;
using NPE.Infrastructure.Common.Data;

namespace NPE.Infrastructure.Common
{
    public class TestStatusUpdateService : ITestStatusUpdateService
    {
        private readonly IStoredProcedureExecutor _spExecutor;

        public TestStatusUpdateService(IStoredProcedureExecutor spExecutor)
        {
            _spExecutor = spExecutor;
        }

        public async Task UpdateCaseTestStatusAsync(
            IEnumerable<long> caseDetailIds,
            int testStatus,
            string userName)
        {
            if (caseDetailIds == null || !caseDetailIds.Any())
                return;

            var idsCsv = string.Join(",", caseDetailIds);

            await _spExecutor.ExecuteAsync<int>(
                "cproc_UpdateCaseTestStatus",
                cmd =>
                {
                    DbHelper.AddParam(cmd, "@IDs", idsCsv);
                    DbHelper.AddParam(cmd, "@TestStatus", testStatus);
                    DbHelper.AddParam(cmd, "@userName", userName);
                });
        }
    }
}