namespace NPE.Core.Modules.Cases.Models;

public sealed class AddCaseChildrenRequest
{
    public long CaseId { get; set; }

    public List<CaseDetailDTO> Details { get; set; }
        = new();

    public List<CaseRemarkDTO> Remarks { get; set; }
        = new();

    public List<CaseClinicalDetailDTO> ClinicalFindings
    { get; set; } = new();

    public CasePaymentDTO? Payment { get; set; }

    public string? ModifiedBy { get; set; }
}