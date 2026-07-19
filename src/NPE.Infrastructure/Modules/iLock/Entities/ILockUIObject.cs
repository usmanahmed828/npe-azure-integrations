using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_UIObject")]
public partial class ILockUIObject
{
    [Key]
    [Column("UIObjectId")]
    public int UIObjectId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DisplayName { get; set; } = null!;

    [StringLength(300)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("UIContainerId")]
    public int UIContainerId { get; set; }

    [ForeignKey("UicontainerId")]
    public virtual ILockUIContainer UIContainer { get; set; } = null!;
}
