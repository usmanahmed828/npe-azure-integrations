namespace NPE.Core.Modules.Laboratory.SampleProcessing.DTOs
{
    public class SampleProcessingSearchRequest
    {
        public string SampleNumber { get; set; }
        public string StatusID { get; set; } = "";
    }
}