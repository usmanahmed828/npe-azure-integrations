namespace NPE.Core.Modules.Patients.Models
{
    //public class PatientSearchRequest
    //{
    //    public string Mobile { get; set; } = null!;
    //    public string? Name { get; set; }
    //}

    //public class PatientSearchParamsDto
    //{
    //    //public string Mobile { get; set; } = string.Empty;
    //    //public int Page { get; set; } = 1;
    //    //public int PageSize { get; set; } = 25;
    //}

    public class PatientSearchRequest
    {
        public string? PatientNumber { get; set; }
        public string? FullName { get; set; }
        //public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Nic { get; set; }
        public string? CardNumber { get; set; }
        public int? Location { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
    public class PatientSearchDTO
    {
        public long Id { get; set; }
        public string PatientNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string FhName { get; set; } = "";
        public string AgeSex { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Center { get; set; } = "";
        public string? CardNumber { get; set; }
        public string? Nic { get; set; }
        public string? Email { get; set; }
    }
}
