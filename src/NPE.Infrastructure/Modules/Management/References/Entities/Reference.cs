using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Management.Centers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Management.Reference.Entities;

/// <summary>
/// Reference is the company who will be paying us per month or lumsum or the patients coming  with the reference will be discounted as dealed with company.
/// </summary>
[Table("Reference")]
public partial class Reference
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    public int? City { get; set; }

    public int? Country { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("RateTypeID")]
    public short RateTypeId { get; set; }

    /// <summary>
    /// 0 For No Credit, 1 For Bill to Company 
    /// </summary>
    public byte PaymentMode { get; set; }

    /// <summary>
    /// Maximum Amount which can be credit
    /// </summary>
    [Column(TypeName = "decimal(15, 5)")]
    public decimal CreditLimit { get; set; }

    public short CreditDays { get; set; }

    /// <summary>
    /// Calculated Using trigger on ReferencePayments
    /// </summary>
    [Column(TypeName = "decimal(15, 5)")]
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Default Discount in Percentage which will be given to patients when they come through reference
    /// </summary>
    [Column(TypeName = "decimal(5, 2)")]
    public decimal DefaultDiscount { get; set; }

    /// <summary>
    /// Max Discount in Percentage which can be  given to patients when they come through reference
    /// </summary>
    [Column(TypeName = "decimal(5, 2)")]
    public decimal MaxDiscount { get; set; }

    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? ContactPhone { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string? ContactMobile { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ContactEmail { get; set; }

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

    public bool Status { get; set; }

    [InverseProperty("Reference")]
    public virtual ReferenceSetting? ReferenceSetting { get; set; }
    public ICollection<CenterReference> CenterReferences { get; set; } = new List<CenterReference>();
}
