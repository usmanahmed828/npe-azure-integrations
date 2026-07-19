using System.ComponentModel.DataAnnotations.Schema;

public class SearchPatientListDTO
{
    public long PatientID { get; set; }
    [Column("Patient #")]
    public string PatientNumber { get; set; } = string.Empty;            // [Patient #]
    [Column("P. Name")]
    public string PatientName { get; set; } = string.Empty;              // [P. Name]

    public bool FamilyCard { get; set; }                                  // ISNULL(pc.FamilyCard,0)

    [Column("Age/Sex")]
    public string AgeSex { get; set; } = string.Empty;                   // [Age/Sex]

    public string Phone { get; set; } = string.Empty;                    // Phone + Mobile

    public string? CardNumber { get; set; }                              // pc.CardNumber

    public long CaseID { get; set; }

    [Column("Case Number")]
    public string CaseNumber { get; set; } = string.Empty;               // [Case Number]
    [Column("Case Reg. Date")]
    public DateTime CaseRegistrationDate { get; set; }                   // [Case Reg. Date]
    [Column("Case Reg. Loc.")]
    public string CaseRegistrationLocation { get; set; } = string.Empty; // [Case Reg. Loc.] (varchar: "ID Name")
    [Column("Total Amount")]
    public decimal TotalAmount { get; set; }                             // c.TotalAmount

    public decimal Discount { get; set; }                                // c.TotalAmount * c.Discount/100

    public decimal Less { get; set; }                                    // c.Less

    public decimal Net { get; set; }                                     // [Net] → c.NetAmount

    public decimal Paid { get; set; }                                    // [Paid] → c.PaidAmount

    public decimal Due { get; set; }                                     // c.Due

    [Column("Due Received")]
    public decimal DueReceived { get; set; }                             // [Due Received]
    [Column("Bank Due Received")]
    public decimal BankDueReceived { get; set; }                         // [Bank Due Received]
    [Column("Bank Paid")]
    public decimal BankPaid { get; set; }                                // [Bank Paid]

    public string CreatedBy { get; set; } = string.Empty;                // c.CreatedBy

    public DateTime CreatedDate { get; set; }                            // c.CreatedDate

    public string? MedicalRecordNo { get; set; }                         // p.MedicalRecordNo

    public string? CABGNo { get; set; }                                  // p.CABGNo

    public bool CanDueReceive { get; set; }                              // computed bit

    public bool ReceivePayment { get; set; }                             // dbo.AllowReceivePayment

    public bool AllowCashierMenu { get; set; }                           // dbo.AllowReceivePayment

    public bool AllowFamilyCard { get; set; }                            // dbo.IsAllowFamilyBlueCard

    public bool AllowPatientMapping { get; set; }                        // dbo.IsAllowMappedBlueCard

    public int CenterID { get; set; }                                    // cn.ID
}