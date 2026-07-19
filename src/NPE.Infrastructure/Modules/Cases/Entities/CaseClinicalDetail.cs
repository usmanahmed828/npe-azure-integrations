using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores clinical findings / notes linked to a case.
/// </summary>
[Table("CaseClinicalDetail")]
public partial class CaseClinicalDetail
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseID")]
    public long CaseId { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string ClinicalDetailCode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

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

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}