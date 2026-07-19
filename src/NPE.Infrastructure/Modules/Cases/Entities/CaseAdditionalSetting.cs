using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores extended optional settings for a case.
/// </summary>
[Table("CaseAdditionalSetting")]
public partial class CaseAdditionalSetting
{
    [Key]
    [Column("CaseID")]
    public long CaseId { get; set; }

    [Column("SecondReferenceID")]
    public int? SecondReferenceId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SecondReferenceName { get; set; }

    [Column("SecondConsultantID")]
    public int? SecondConsultantId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SecondConsultantName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MedicalRecordNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SampleReceivedFrom { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SampleReceivedBy { get; set; }

    [Column("PONumber")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ponumber { get; set; }

    [Unicode(false)]
    public string? CaseSettings { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}