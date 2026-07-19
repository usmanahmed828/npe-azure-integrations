using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[PrimaryKey("CompanyId", "RightId", "UIObjectId")]
[Table("iLock_RightUIObject")]
public partial class ILockRightUIObject
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int RightId { get; set; }

    [Key]
    [Column("UIObjectId")]
    public int UIObjectId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }
}
