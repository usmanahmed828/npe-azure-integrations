using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Common.Identity.Entities;

[PrimaryKey("CenterCode", "Type", "StartValue")]
[Table("Identity")]
public partial class Identity
{
    [Key]
    public int CenterCode { get; set; }

    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Key]
    public long StartValue { get; set; }

    public long EndValue { get; set; }

    public long CurrentValue { get; set; }
}
