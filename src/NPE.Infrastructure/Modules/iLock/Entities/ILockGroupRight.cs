using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("CompanyId", "RightId", "GroupId")]
[Table("iLock_GroupRight")]
public partial class ILockGroupRight
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int RightId { get; set; }

    [Key]
    public int GroupId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("GroupId, CompanyId")]
    [InverseProperty("ILockGroupRights")]
    public virtual ILockGroup ILockGroup { get; set; } = null!;

    [ForeignKey("RightId, CompanyId")]
    [InverseProperty("ILockGroupRights")]
    public virtual ILockRight ILockRight { get; set; } = null!;
}
