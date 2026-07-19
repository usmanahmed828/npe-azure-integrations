using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Users.Entities;

[PrimaryKey("UserId", "CompanyId")]
[Table("iLock_User")]
public partial class ILockUser
{
    [Key]
    public int CompanyId { get; set; }

    [Key]
    public int UserId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? LastName { get; set; }

    public bool Disabled { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Password { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Address { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column("modifiedDate", TypeName = "smalldatetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("ILockUser")]
    public virtual ICollection<ILockUserHistory> ILockUserHistories { get; set; } = new List<ILockUserHistory>();
}
