using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("GroupId", "CompanyId")]
[Table("iLock_Group")]
public partial class ILockGroup
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int GroupId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("ILockGroups")]
    public virtual ILockCompany Company { get; set; } = null!;

    //[InverseProperty("ILockGroup")]
    //public virtual ICollection<ILockGroupRight> ILockGroupRights { get; set; } = new List<ILockGroupRight>();

    [InverseProperty("ILockGroup")]
    public virtual ICollection<ILockGroupUser> ILockGroupUsers { get; set; } = new List<ILockGroupUser>();
    public ICollection<ILockGroupUIObject> Permissions { get; set; } = new List<ILockGroupUIObject>();
}
