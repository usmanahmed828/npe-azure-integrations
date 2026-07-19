using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Cases.Entities;

/// <summary>
/// Stores online payment request / callback information for a case.
/// </summary>
[Table("CasePaymentOnline")]
[Index(nameof(CaseId), nameof(PaymentType), nameof(ModifiedBy),
    Name = "Index_7720233")]
public partial class CasePaymentOnline
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CaseID")]
    public long CaseId { get; set; }

    public byte Method { get; set; }

    [Column(TypeName = "decimal(10,0)")]
    public decimal Amount { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string ModifiedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime ModifiedDate { get; set; }

    [Column("PaymentURL")]
    public string? PaymentUrl { get; set; }

    /// <summary>
    /// 0 = No, 1 = Received, 2 = Error
    /// </summary>
    public byte? IsReceived { get; set; }

    public bool? IsAlertSent { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? AlertSentDate { get; set; }

    public int? PaymentType { get; set; }

    #region Navigation Properties

    public virtual Case Case { get; set; } = null!;

    #endregion
}