namespace NPE.Core.Modules.Laboratory.ResultEntry.DTOs
{
    public class ResultEntrySearchRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool FilterByDate { get; set; }

        public string TestCodeFrom { get; set; } = "";
        public string TestCodeTo { get; set; } = "";

        public string TestName { get; set; } = "";

        public string CaseNumber { get; set; } = "";

        public int RegistrationLocation { get; set; }

        public string UserID { get; set; } = "";

        public string TestStatus { get; set; } = "";

        public string IsDelayed { get; set; } = "-1";

        public string ConnectedCenters { get; set; } = "";

        public bool ForConnectedCenters { get; set; }

        public bool IsSendToHeadOffice { get; set; }

        public bool IsForAbnormalValue { get; set; }

        public int ConductedAt { get; set; } = 1001;

        public int Consultant { get; set; }

        public int Reference { get; set; }

        //public string PatientName { get; set; } = "";
    }
}