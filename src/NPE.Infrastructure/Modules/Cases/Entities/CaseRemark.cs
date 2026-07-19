using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores remarks, internal notes, and additional charges for a case.
/// </summary>
[Table("CaseRemark")]
public partial class CaseRemark
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column("RemarkID")]
    public short RemarkId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    public bool Type { get; set; }

    [Column(TypeName = "decimal(15,2)")]
    public decimal? Rate { get; set; }

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

    public bool? Status { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}