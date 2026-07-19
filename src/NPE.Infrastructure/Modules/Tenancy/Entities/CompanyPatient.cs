using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Tenancy.Entities
{
    [PrimaryKey(nameof(CompanyId), nameof(PatientId))]
    [Table("CompanyPatients", Schema = "tenant")]
    public class CompanyPatient
    {
        public int CompanyId { get; set; }

        public long PatientId { get; set; }
    }
}
