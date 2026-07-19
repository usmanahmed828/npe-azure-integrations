using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey("CenterId", "ReportLayoutId")]
[Table("CenterReportLayoutMapping")]
public partial class CenterReportLayoutMapping
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    [Key]
    [Column("ReportLayoutID")]
    public int ReportLayoutId { get; set; }
    public Center? Center { get; set; }
    //public ReportLayout? ReportLayout { get; set; } = null;
}
