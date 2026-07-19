using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_Application")]
public partial class ILockApplication
{
    [Key]
    public int ApplicationId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("Application")]
    public virtual ICollection<ILockUIContainer> ILockUIContainers { get; set; } = new List<ILockUIContainer>();
}
