namespace NPE.Core.Modules.Laboratory.SampleProcessing.DTOs
{
    public class SampleProcessingTestDto
    {
        public long ID { get; set; }
        public long CaseID { get; set; }
        public long TestID { get; set; }

        public string TestName { get; set; }
        public int TestStatus { get; set; }
        public string Code { get; set; }

        public string Comments { get; set; }

        public int TemplateID { get; set; }
        public int DepartmentID { get; set; }

        public bool IsDelayed { get; set; }
        public int ConductedAt { get; set; }

        public string SampleCode { get; set; }
        public string CaseNumber { get; set; }
    }
}