using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores tests/items registered against a case.
/// </summary>
[Table("CaseDetail")]
[Index(nameof(Status), nameof(TemplateId), Name = "IDX_CaseDetail")]
public partial class CaseDetail
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column("TestID")]
    public int TestId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string TestName { get; set; } = null!;

    [Column(TypeName = "decimal(15,2)")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Current processing status of test.
    /// </summary>
    public short TestStatus { get; set; }

    /// <summary>
    /// Location where test is conducted.
    /// </summary>
    public int ConductedAt { get; set; }

    /// <summary>
    /// Expected reporting date.
    /// </summary>
    [Column(TypeName = "smalldatetime")]
    public DateTime ReportingDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    [Column("TemplateID")]
    public short TemplateId { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Comments { get; set; }

    public bool IsDelayed { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ConductedBy { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ApprovedBy { get; set; }

    [Column("ExternalSystemDetailID")]
    [StringLength(64)]
    [Unicode(false)]
    public string? ExternalSystemDetailId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? SyncDateTime { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    public virtual CaseDetailInstrument? CaseDetailInstrument { get; set; }

    public virtual OutsourceCaseDetail? OutsourceCaseDetail { get; set; }

    #endregion
}