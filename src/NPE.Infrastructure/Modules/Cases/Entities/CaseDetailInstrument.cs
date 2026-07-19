using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores instrument assignment/details for a case test.
/// </summary>
[Table("CaseDetailInstrument")]
public partial class CaseDetailInstrument
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseDetailID")]
    public long CaseDetailId { get; set; }

    [Column("InstrumentID")]
    public int InstrumentId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedDate { get; set; }

    #region Navigation Properties

    public virtual CaseDetail CaseDetail { get; set; } = null!;

    #endregion
}