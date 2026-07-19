using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Users.Entities;

[PrimaryKey("HistroyId", "CompanyId")]
[Table("iLock_UserHistory")]
public partial class ILockUserHistory
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int HistroyId { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SiteDescription { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckIn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CheckOut { get; set; }

    public int ApplicationId { get; set; }

    [ForeignKey("UserId, CompanyId")]
    [InverseProperty("ILockUserHistories")]
    public virtual ILockUser ILockUser { get; set; } = null!;
}
