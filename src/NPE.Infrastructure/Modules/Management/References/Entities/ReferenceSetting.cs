using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Reference.Entities;

[Table("ReferenceSetting")]
public partial class ReferenceSetting
{
    [Key]
    [Column("ReferenceID")]
    public int ReferenceId { get; set; }

    public bool IsPrescriptionEnabled { get; set; }

    public bool IsCouponEnabled { get; set; }

    public bool Status { get; set; }

    public bool? IsExtendedSearchEnabled { get; set; }

    public bool? IsLoyaltyCardEnabled { get; set; }

    public bool IsOutsourceRequestEnabled { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CourierName { get; set; }

    public bool? IsAllowReportAccess { get; set; }

    public bool? SecondaryReference { get; set; }

    public bool? AdditionalInfo { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? AdditionalInfoValidationFields { get; set; }

    public string? Settings { get; set; }

    [ForeignKey("ReferenceId")]
    [InverseProperty("ReferenceSetting")]
    public virtual Reference Reference { get; set; } = null!;
}
