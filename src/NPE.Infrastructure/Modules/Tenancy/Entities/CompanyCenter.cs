using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Tenancy.Entities;

[PrimaryKey(nameof(CompanyId), nameof(CenterId))]
[Table("CompanyCenters", Schema = "tenant")]
public partial class CompanyCenter
{
    [Key]
    public int CompanyId
    { get; set; }

    [Key]
    [Column("CenterID")]
    public int CenterId { get; set; }

    public bool IsRootCenter
    { get; set; }

    #region Navigation

    public virtual Center?
        Center
    { get; set; }

    public virtual ILockCompany?
        Company
    { get; set; }

    #endregion
}