using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey(nameof(CenterId), nameof(ConsultantId))]
[Table("CenterConsultant")]
public partial class CenterConsultant
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    [Key]
    [Column("ConsultantID")]
    public int ConsultantId { get; set; }

    public Center? Center { get; set; }
    public Consultant.Entities.Consultant? Consultant { get; set; }
}
