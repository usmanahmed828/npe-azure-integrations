namespace NPE.Core.Modules.Patients.Models
{
    public class PatientSearchResult
    {
        public long Id { get; set; }
        public string PatientNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string? Mobile { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class PatientInfoDto
    {
        //[JsonProperty("mrNumber")]
        public string MedicalRecordNumber { get; set; }
        //[JsonProperty("id")]
        public long PatientID { get; set; }
        public string PatientNumber { get; set; } = string.Empty;
        //[JsonProperty("name")]
        public string FullName { get; set; } = string.Empty;
        //[JsonProperty("mobile")]
        public string Mobile { get; set; } = string.Empty;
        //public string NIC { get; set; } = string.Empty;
        //[JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        //[JsonProperty("age")]
        public int Age { get; set; }
        //[JsonProperty("gender")]
        public string Sex { get; set; } = string.Empty;
        //[JsonProperty("caseNumber")]
        public string CaseNumber { get; set; } = string.Empty;
        //[JsonProperty("recentVisit")]
        public string RecentVisit { get; set; } = string.Empty;

    }
}
