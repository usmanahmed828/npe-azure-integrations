namespace NPE.Core.Common
{
    public interface ICaseStatusWorkflowService
    {
        Task<List<int>> GetNextStatusesAsync(int currentStatus);
    }
}