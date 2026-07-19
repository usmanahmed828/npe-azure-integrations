namespace NPE.Core.Modules.Laboratory.BatchProcessing.DTOs
{
    public class BatchProcessingSearchRequest
    {
        public string CaseNumber { get; set; } = "";
        public string TestStatus { get; set; } = "";
        public int DispatchNo { get; set; } = 0;
    }
}