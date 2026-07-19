using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.Management.Reference.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Tenancy
{
    [PrimaryKey(nameof(CompanyId), nameof(ReferenceId))]
    [Table("CompanyReferences", Schema = "tenant")]
    public class CompanyReference
    {
        public int CompanyId
        { get; set; }

        public int ReferenceId
        { get; set; }

        #region Navigation

        public virtual ILockCompany
            Company
        { get; set; } = null!;

        public virtual Reference
            Reference
        { get; set; } = null!;

        #endregion
    }
}