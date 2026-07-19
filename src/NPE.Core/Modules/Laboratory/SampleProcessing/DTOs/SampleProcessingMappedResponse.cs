namespace NPE.Core.Modules.Laboratory.SampleProcessing.DTOs
{
    public class SampleProcessingMappedResponse
    {
        public long ID { get; set; }

        public string PName { get; set; }
        public string FHName { get; set; }
        public string AgeSex { get; set; }
        public string BloodGroup { get; set; }

        public long CaseID { get; set; }
        public string CaseNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int RegLoc { get; set; }
        public string CenterName { get; set; }

        public int RemarkID { get; set; }
        public string CaseRemarks { get; set; }

        public string SampleNumber { get; set; }
        public string SampleCode { get; set; }

        public List<SampleProcessingTestDto> Tests { get; set; } = new();
    }
}