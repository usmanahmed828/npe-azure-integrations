using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Management.Centers;

[Table("CentersToCheckDueAmountFor")]
public partial class CentersToCheckDueAmountFor
{
    [Key]
    [Column("CenterCode")]
    public int CenterId
    { get; set; }

    #region Navigation

    public virtual Center?
        Center
    { get; set; }

    #endregion
}