namespace NPE.Core.Modules.Laboratory.ResultEntry.DTOs
{
    public class ResultEntryMappedResponse
    {
        public long Id { get; set; }
        public string PName { get; set; } = string.Empty;
        public string FHName { get; set; } = string.Empty;
        public string AgeSex { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;

        public long CaseID { get; set; }
        public string CaseNumber { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public int RegLoc { get; set; }

        public string CenterName { get; set; } = string.Empty;

        public int RemarkID { get; set; }

        public List<ResultEntryTestDto> Tests { get; set; } = new();
    }
}