using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPE.Infrastructure.Modules.Tenancy.Entities
{
    [PrimaryKey(nameof(CompanyId), nameof(TestId))]
    [Table("CompanyTests", Schema = "tenant")]
    public class CompanyTest
    {
        public int CompanyId { get; set; }

        public int TestId { get; set; }
    }
}
