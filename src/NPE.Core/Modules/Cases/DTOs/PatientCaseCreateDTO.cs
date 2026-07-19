using NPE.Core.Modules.Cases.Models;
using NPE.Core.Modules.Patients.Models;

namespace NPE.Core.Modules.Cases.DTOs
{
    public class PatientCaseCreateDTO
    {
        public PatientDTO Patient { get; set; } = null!;
        public CaseDTO Case { get; set; } = null!;
    }
    public sealed class PatientCaseCreateResult
    {
        public long PatientId { get; set; }

        public long CaseId { get; set; }
    }
}
