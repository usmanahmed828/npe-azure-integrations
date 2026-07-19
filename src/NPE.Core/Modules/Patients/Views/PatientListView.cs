using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Patients.Views
{
    public class PatientListView
    {
        public long Id { get; set; }
        public string PatientNumber { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Fhname { get; set; } = "";
        public string AgeSex { get; set; } = "";
        public string? Nic { get; set; }
        public string? Phone { get; set; }
        public string? CardNumber { get; set; }
        public DateTime DateRegistered { get; set; }
        public int Location { get; set; }
        public string Center { get; set; } = "";
        public byte Sex { get; set; }
        public int? UserID { get; set; }
        public string? Email { get; set; }
    }
}
