namespace NPE.Core.Modules.Laboratory.BatchProcessing.DTOs
{
    public class BatchProcessingMappedResponse
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

        public List<BatchProcessingTestDto> Tests { get; set; } = new();
    }
}