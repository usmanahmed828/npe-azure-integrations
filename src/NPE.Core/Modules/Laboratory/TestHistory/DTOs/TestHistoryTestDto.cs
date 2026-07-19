using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Laboratory.TestHistory.DTOs
{
    public class TestHistoryTestDto
    {
        public long CaseID { get; set; }
        public long CaseDetailID { get; set; }

        [Column("Test Code")]
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string Status { get; set; }

        public string Comments { get; set; }
        public int TestStatus { get; set; }

        public int TemplateId { get; set; }
        public int ConductedAt { get; set; }
        public bool IsPrinted { get; set; }
    }
}
