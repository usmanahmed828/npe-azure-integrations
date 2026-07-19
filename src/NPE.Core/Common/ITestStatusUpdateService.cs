namespace NPE.Core.Common
{
    public interface ITestStatusUpdateService
    {
        Task UpdateCaseTestStatusAsync(
            IEnumerable<long> caseDetailIds,
            int testStatus,
            string userName);
    }
}