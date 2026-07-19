using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("CompanyId", "GroupId", "UIObjectId")]
[Table("iLock_GroupUIObject")]
public partial class ILockGroupUIObject
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int GroupId { get; set; }

    [Key]
    [Column("UIObjectId")]
    public int UIObjectId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    public ILockGroup Group { get; set; } = null!;
    public ILockUIObject UIObject { get; set; } = null!;
}
