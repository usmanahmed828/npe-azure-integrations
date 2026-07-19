using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Management.Consultant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey(nameof(CenterId), nameof(ReferenceId))]
[Table("CenterReference")]
public partial class CenterReference
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }
    [Key]
    [Column("ReferenceID")]
    public int ReferenceId { get; set; }
    public Center? Center { get; set; }
    public Reference.Entities.Reference? Reference { get; set; } = null;
}
