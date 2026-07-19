using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores auxiliary system flags/settings for a case.
/// </summary>
[Table("CaseSetting")]
public partial class CaseSetting
{
    [Key]
    [Column("CaseID")]
    public long CaseId { get; set; }

    public bool? IsCompleted { get; set; }

    public bool? IsAlertSent { get; set; }

    public bool? IsEmailSent { get; set; }

    [Column("BBOrderNumber")]
    [StringLength(80)]
    [Unicode(false)]
    public string? BborderNumber { get; set; }

    [Column("BBType")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Bbtype { get; set; }

    public bool? Status { get; set; }

    [Column("BBDIN")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Bbdin { get; set; }

    [Column("QRString")]
    [StringLength(800)]
    [Unicode(false)]
    public string? Qrstring { get; set; }

    [Column("QRImage", TypeName = "image")]
    public byte[]? Qrimage { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}