using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.Tenancy;
using NPE.Infrastructure.Modules.Tenancy.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.iLock.Entities;

[Table("iLock_Company")]
public partial class ILockCompany
{
    [Key]
    public int CompanyId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<ILockGroup> ILockGroups { get; set; } = new List<ILockGroup>();

    //[InverseProperty("Company")]
    //public virtual ICollection<ILockRight> ILockRights { get; set; } = new List<ILockRight>();
    public ICollection<CompanyCenter> CompanyCenters { get; set; } = new List<CompanyCenter>();
    public ICollection<CompanyConsultant> CompanyConsultants { get; set; } = new List<CompanyConsultant>();
    public ICollection<CompanyReference> CompanyReferences { get; set; } = new List<CompanyReference>();
}
