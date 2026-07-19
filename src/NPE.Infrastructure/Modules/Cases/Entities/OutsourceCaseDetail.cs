using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores outsource / dispatch information for a case detail.
/// </summary>
[Table("OutsourceCaseDetail")]
public partial class OutsourceCaseDetail
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseDetailID")]
    public long CaseDetailId { get; set; }

    [Column("DispatchID")]
    public long DispatchId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime DispatchDate { get; set; }

    public short DispatchStatus { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string DispatchClient { get; set; } = null!;

    #region Navigation Properties

    public virtual CaseDetail CaseDetail { get; set; } = null!;

    #endregion
}