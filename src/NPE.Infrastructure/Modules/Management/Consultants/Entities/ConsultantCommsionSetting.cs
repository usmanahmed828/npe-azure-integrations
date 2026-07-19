using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management;

[Table("ConsultantCommsionSetting")]
public partial class ConsultantCommsionSetting
{
    [Key]
    [Column("ConsultantID")]
    public int ConsultantId { get; set; }

    [Column(TypeName = "money")]
    public decimal? RoutineCommission { get; set; }

    [Column(TypeName = "money")]
    public decimal? SpecialCommission { get; set; }

    [Column("PCRCommission", TypeName = "money")]
    public decimal? Pcrcommission { get; set; }

    [Column(TypeName = "money")]
    public decimal? BiopsyCommission { get; set; }

    [Column(TypeName = "money")]
    public decimal? RadiologyCommission { get; set; }

    [Column(TypeName = "money")]
    public decimal? OtherCommission { get; set; }

    [Column(TypeName = "money")]
    public decimal? OtherTestCommission { get; set; }
}
