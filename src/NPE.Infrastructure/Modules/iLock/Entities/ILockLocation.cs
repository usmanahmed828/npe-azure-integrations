using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("LocationId", "CompanyId")]
[Table("iLock_Location")]
public partial class ILockLocation
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string LocationId { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }
}
