using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[Table("CenterPayment")]
public partial class CenterPayment
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CenterID")]
    public int CenterId { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// 0 For Cash, 1 for Cheque, 2 for Draft
    /// </summary>
    public byte Method { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime ReceivedDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ReceivedBy { get; set; } = null!;

    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    /// <summary>
    /// Cheque No or Credit Card No
    /// </summary>
    [Column("CNo")]
    [StringLength(64)]
    [Unicode(false)]
    public string? Cno { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime CreatedDate { get; set; }
     public Center? Center { get; set; }
}
