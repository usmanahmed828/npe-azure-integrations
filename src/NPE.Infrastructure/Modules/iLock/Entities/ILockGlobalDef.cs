using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_GlobalDef")]
public partial class ILockGlobalDef
{
    [Key]
    [Column("globalID")]
    public int GlobalId { get; set; }

    public bool? EnableLocation { get; set; }

    public bool? EnableCompany { get; set; }
}
