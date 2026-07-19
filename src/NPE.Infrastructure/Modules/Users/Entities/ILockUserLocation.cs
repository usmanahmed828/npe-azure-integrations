using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Users.Entities;
[PrimaryKey("CompanyId", "UserId", "LocationId")]
[Table("iLock_UserLocation")]
public partial class ILockUserLocation
{
    public int CompanyId { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LocationId { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }
}
