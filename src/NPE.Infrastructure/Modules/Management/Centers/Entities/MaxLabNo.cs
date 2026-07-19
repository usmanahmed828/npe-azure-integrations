using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey("CenterCode", "Dated")]
public partial class MaxLabNo
{
    [Key]
    public int CenterCode { get; set; }

    [Key]
    [Column(TypeName = "smalldatetime")]
    public DateTime Dated { get; set; }

    public int NextLabNo { get; set; }
}
