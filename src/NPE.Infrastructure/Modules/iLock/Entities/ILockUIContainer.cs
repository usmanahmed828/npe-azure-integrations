using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_UIContainer")]
public partial class ILockUIContainer
{
    [Key]
    [Column("UIContainerId")]
    public int UIContainerId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DisplayName { get; set; } = null!;

    [StringLength(300)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [StringLength(300)]
    [Unicode(false)]
    public string? Description { get; set; }

    public int? ApplicationId { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("ILockUIContainers")]
    public virtual ILockApplication? Application { get; set; }

    public ICollection<ILockUIObject> UIObjects { get; set; } = new List<ILockUIObject>();
}
