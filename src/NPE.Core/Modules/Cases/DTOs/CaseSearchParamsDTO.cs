public class CaseSearchParamsDto
{
    public string? PatientNumber { get; set; }
    public string? PatientName { get; set; }
    public int? Sex { get; set; } = 3;
    public string? BloodGroup { get; set; } = "All";
    public string? Phone { get; set; }
    public string? NIC { get; set; }
    public int? RegistrationCenter { get; set; }
    public DateTime? RegistrationDateFrom { get; set; }
    public DateTime? RegistrationDateTo { get; set; }
    public bool? FilterByDate { get; set; } = false;
    public string? CaseNumber { get; set; }
    public bool? BankCases { get; set; }
    public int? CaseRegLocation { get; set; }
    public int? ConsultantID { get; set; }
    public int? ReferenceID { get; set; }
    public DateTime? CaseRegFromDate { get; set; }
    public DateTime? CaseRegToDate { get; set; }
    public bool? CaseRegFilterByDate { get; set; }
    public string? UserName { get; set; }
    public string? MRNo { get; set; }
    public string? CABGNo { get; set; }
    public int? UserID { get; set; }
    public string? CardNumber { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}