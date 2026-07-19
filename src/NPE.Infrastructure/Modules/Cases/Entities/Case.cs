using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Patients.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores patient case header information.
/// Amount fields are aggregate values updated by workflow/triggers.
/// </summary>
[Table("Case")]
[Index(nameof(RegistrationLocation), nameof(RegistrationDate), Name = "IDX_Case")]
[Index(nameof(RegistrationDate), Name = "IDX_Case_RegistrationDate")]
[Index(nameof(CreatedDate), Name = "IX_Case_CreatedDate_ID")]
public partial class Case
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string CaseNumber { get; set; } = null!;

    [Column("PatientID")]
    public long PatientId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime RegistrationDate { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime ReportingDate { get; set; }

    [Column("ReferenceID")]
    public int? ReferenceId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ReferenceName { get; set; } = null!;

    [Column("ConsultantID")]
    public int? ConsultantId { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string ConsultantName { get; set; } = null!;

    public int RegistrationLocation { get; set; }

    public int DestinationLocation { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalAmount { get; set; }

    public byte Discount { get; set; }

    [Column(TypeName = "money")]
    public decimal Less { get; set; }

    [Column(TypeName = "money")]
    public decimal NetAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? PaidAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal Due { get; set; }

    public bool Completed { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Comments { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    [Column(TypeName = "money")]
    public decimal? BankDueReceived { get; set; }

    [Column(TypeName = "money")]
    public decimal? BankPaid { get; set; }

    [Column(TypeName = "money")]
    public decimal? DueReceived { get; set; }

    public bool AlertSent { get; set; }

    public bool? WithoutHistory { get; set; }

    #region Navigation Properties
    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<CaseDetail> CaseDetails { get; set; }
        = new List<CaseDetail>();

    public virtual ICollection<CasePayment> CasePayments { get; set; }
        = new List<CasePayment>();

    public virtual ICollection<CaseClinicalDetail> CaseClinicalDetails { get; set; }
        = new List<CaseClinicalDetail>();

    public virtual ICollection<CaseRemark> CaseRemarks { get; set; }
        = new List<CaseRemark>();

    public virtual CaseAdditionalSetting? AdditionalSetting { get; set; }

    public virtual CaseInfo? CaseInfo { get; set; }

    public virtual CasePaymentOnline? PaymentOnline { get; set; }

    public virtual CaseSetting? CaseSetting { get; set; }

    public virtual CorporatePaymentFinancial? CorporatePaymentFinancial { get; set; }

    #endregion
}