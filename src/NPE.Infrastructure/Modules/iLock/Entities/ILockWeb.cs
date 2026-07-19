using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_Web")]
public partial class ILockWeb
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("UIContainer")]
    [StringLength(100)]
    public string UIContainer { get; set; } = null!;

    [Column("UIObject")]
    [StringLength(100)]
    public string UIObject { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Value { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }
}
