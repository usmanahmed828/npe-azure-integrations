namespace NPE.Core.Modules.Laboratory.ResultEntry.DTOs
{
    public class ResultEntryPatientDto
    {
        public long Id { get; set; }

        public string PName { get; set; } = "";

        public string FHName { get; set; } = "";

        public string AgeSex { get; set; } = "";

        public string BloodGroup { get; set; } = "";

        public long CaseID { get; set; }

        public string CaseNumber { get; set; } = "";

        public DateTime RegistrationDate { get; set; }

        public int RegLoc { get; set; }

        public string CenterName { get; set; } = "";

        public int RemarkID { get; set; }

        public int AllowProcessingCase { get; set; }
    }

    public class ResultEntryTestDto
    {
        public long ID { get; set; }

        public long CaseID { get; set; }

        public long TestID { get; set; }

        public string TestName { get; set; } = "";

        public int TestStatus { get; set; }

        public string CaseTestStatus { get; set; } = "";

        public string PerformedAt { get; set; } = "";

        public string Code { get; set; } = "";

        public string Comments { get; set; } = "";

        public int TemplateID { get; set; }

        public int DepartmentID { get; set; }

        public bool IsDelayed { get; set; }

        public int ConductedAt { get; set; }

        //public int PatientAtPhlebotomist { get; set; }

        //public int AllowProcessingCase { get; set; }

        //public bool HasPreviousTest { get; set; }
    }

    public class ResultEntrySearchResponse
    {
        public List<ResultEntryPatientDto> Patients { get; set; } = new();

        public List<ResultEntryTestDto> Tests { get; set; } = new();

    }
}