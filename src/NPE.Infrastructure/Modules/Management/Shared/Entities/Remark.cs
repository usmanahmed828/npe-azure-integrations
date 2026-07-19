using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

/// <summary>
/// Remarks related to case and Case Detail it also it will be used to configure other charges.
/// </summary>
[Table("Remark")]
public partial class Remark
{
    [Key]
    [Column("ID")]
    public short Id { get; set; }

    /// <summary>
    /// 1 will be used to indicate that this remark will be used internally for Lab 
    /// </summary>
    public bool? Internal { get; set; }

    public bool? Externals { get; set; }

    /// <summary>
    /// 0 for cmments and 1 for other charges
    /// </summary>
    public bool Type { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    /// <summary>
    /// Charges if any otherwise 0
    /// </summary>
    [Column(TypeName = "decimal(10, 0)")]
    public decimal? Rate { get; set; }

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
}
