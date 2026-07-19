using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Management.Centers;

[PrimaryKey("CenterId", "CenterOwnerId")]
[Table("CenterOwnerCenter")]
public partial class CenterOwnerCenter
{
    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    [Key]
    [Column("CenterOwnerID")]
    public int CenterOwnerId { get; set; }

    public Center? Center { get; set; }
    public CenterOwner? CenterOwner { get; set; } = null;
}
