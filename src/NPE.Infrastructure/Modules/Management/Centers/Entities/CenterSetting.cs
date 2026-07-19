using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[Table("CenterSetting")]
public partial class CenterSetting
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    public int DestinationLocation { get; set; }

    public int DefaultStatus { get; set; }

    [Column("RegionID")]
    public int? RegionId { get; set; }

    public int? TransportTime { get; set; }

    public bool? IsCreditFeatureEnabled { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CreditWarningLimit { get; set; }

    public Center? Center { get; set; }
}
