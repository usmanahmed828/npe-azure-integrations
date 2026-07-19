using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("RightId", "CompanyId")]
[Table("iLock_Right")]
public partial class ILockRight
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int RightId { get; set; }

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
    [InverseProperty("ILockRights")]
    public virtual ILockCompany Company { get; set; } = null!;

    [InverseProperty("ILockRight")]
    public virtual ICollection<ILockGroupRight> ILockGroupRights { get; set; } = new List<ILockGroupRight>();
}
