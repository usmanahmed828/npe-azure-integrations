using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Laboratory.TestHistory.DTOs
{
    public class TestHistoryCaseDto
    {
        public long CaseID { get; set; }
        public long PatientID { get; set; }

        public string PatientNumber { get; set; }

        [Column("Patient Name")]
        public string PatientName { get; set; }

        [Column("Age/Sex")]
        public string AgeSex { get; set; }

        [Column("Bld. Group")]
        public string BloodGroup { get; set; }
        public string Mobile { get; set; }

        [Column("Case #")]
        public string CaseNumber { get; set; }

        [Column("Reg. Date")]
        public DateTime RegistrationDate { get; set; }

        public string CenterName { get; set; }

        public List<TestHistoryTestDto> Tests { get; set; } = new();
    }
}
