using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.Management.Consultant.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Tenancy
{
    [PrimaryKey(nameof(CompanyId), nameof(ConsultantId))]
    [Table("CompanyConsultants", Schema = "tenant")]
    public class CompanyConsultant
    {
        public int CompanyId
        { get; set; }

        public int ConsultantId
        { get; set; }

        #region Navigation

        public virtual ILockCompany
            Company
        { get; set; } = null!;

        public virtual Consultant
            Consultant
        { get; set; } = null!;

        #endregion
    }
}