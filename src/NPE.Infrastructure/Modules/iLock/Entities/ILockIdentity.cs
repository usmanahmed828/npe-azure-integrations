using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Keyless]
[Table("iLock_identity")]
public partial class ILockIdentity
{
    [StringLength(50)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    public int NextId { get; set; }
}
