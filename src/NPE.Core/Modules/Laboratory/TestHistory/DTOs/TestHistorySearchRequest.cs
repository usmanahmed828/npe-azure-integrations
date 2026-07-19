namespace NPE.Core.Modules.Laboratory.TestHistory.DTOs
{
    public class TestHistorySearchRequest
    {
        public string PatientNumber { get; set; }
        public string PatientName { get; set; }
        public short Sex { get; set; }
        public string BloodGroup { get; set; }
        public string Phone { get; set; }
        public string NIC { get; set; }

        public int RegistrationCenter { get; set; }
        public DateTime RegistrationDateFrom { get; set; }
        public DateTime RegistrationDateTo { get; set; }
        public bool FilterByDate { get; set; }

        public string CaseNumber { get; set; }
        public string TestCodeFrom { get; set; }
        public string TestCodeTo { get; set; }
        public string TestName { get; set; }
        public string TestStatus { get; set; }

        public int CaseReglocation { get; set; }
        public int ConsultantID { get; set; }
        public int ReferenceID { get; set; }

        public DateTime CaseRegFromdate { get; set; }
        public DateTime CaseRegToDate { get; set; }
        public bool CaseRegFilterByDate { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string Diagonosis { get; set; }
        public string Specimen { get; set; }
        public string BiopsyNo { get; set; }
        public string CS { get; set; }

        public string MRNo { get; set; }
        public string CABGNo { get; set; }

        public bool IsSpecialCase { get; set; }
        public string TableName { get; set; }
        public bool CriticalResult { get; set; }
    }
}