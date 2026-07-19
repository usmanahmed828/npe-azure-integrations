using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("CompanyId", "UserId", "GroupId")]
[Table("iLock_GroupUser")]
public partial class ILockGroupUser
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int UserId { get; set; }

    [Key]
    public int GroupId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("GroupId, CompanyId")]
    [InverseProperty("ILockGroupUsers")]
    public virtual ILockGroup ILockGroup { get; set; } = null!;
}
