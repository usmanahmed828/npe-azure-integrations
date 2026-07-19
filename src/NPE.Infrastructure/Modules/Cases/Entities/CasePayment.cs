using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores payments received against a case.
/// </summary>
[Table("CasePayment")]
[Index(nameof(CaseId),
    Name = "_dta_index_CasePayment_7_2139206721__K2_4_7")]
public partial class CasePayment
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime Dated { get; set; }

    /// <summary>
    /// 0 Cash, 1 Card, 2 Cheque, 3 Transfer, 4 Waived Off
    /// </summary>
    public byte Method { get; set; }

    /// <summary>
    /// 0 Advance, 1 Due Received, 2 Adjustment
    /// </summary>
    public byte Type { get; set; }

    [Column(TypeName = "decimal(10,0)")]
    public decimal Amount { get; set; }

    [Column("CNo")]
    [StringLength(64)]
    [Unicode(false)]
    public string? Cno { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("CenterID")]
    public int? CenterId { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}