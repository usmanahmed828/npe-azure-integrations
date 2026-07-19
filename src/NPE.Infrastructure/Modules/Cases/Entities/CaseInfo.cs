using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores technical/session metadata captured during case registration.
/// </summary>
[Table("CaseInfo")]
public partial class CaseInfo
{
    [Key]
    [Column("CaseID")]
    public long CaseId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Server { get; set; } = null!;

    [Column("ClientIP")]
    [StringLength(50)]
    [Unicode(false)]
    public string ClientIp { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [Column("UserID")]
    public long UserId { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}