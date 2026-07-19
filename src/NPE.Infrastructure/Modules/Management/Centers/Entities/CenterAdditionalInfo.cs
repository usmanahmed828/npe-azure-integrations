using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey("CenterId", "UserId")]
[Table("CenterAdditionalInfo")]
public partial class CenterAdditionalInfo
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    [StringLength(10)]
    public string FlightNo { get; set; } = null!;

    [StringLength(10)]
    public string TestCode { get; set; } = null!;

    [Column("TestID")]
    public int TestId { get; set; }

    [Column("ReferenceID")]
    public int ReferenceId { get; set; }

    [StringLength(10)]
    public string ReferenceCode { get; set; } = null!;

    public int AdditionalHoursInCurrentTime { get; set; }

    [Column("ConsultantID")]
    public int? ConsultantId { get; set; }

    [StringLength(10)]
    public string? ConsultantCode { get; set; }

    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FlightDate { get; set; }

    public Center? Center { get; set; }
}
