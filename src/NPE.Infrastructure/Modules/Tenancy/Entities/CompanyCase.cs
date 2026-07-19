using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Tenancy.Entities
{
    [PrimaryKey(nameof(CompanyId), nameof(CaseId))]
    [Table("CompanyCases", Schema = "tenant")]
    public class CompanyCase
    {
        public int CompanyId { get; set; }

        public long CaseId { get; set; }
    }
}
