using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

[Table("Region")]
public partial class Region
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("RManagerName")]
    [StringLength(50)]
    [Unicode(false)]
    public string? RmanagerName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ContactNo { get; set; }

    public bool? Status { get; set; }

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
}
