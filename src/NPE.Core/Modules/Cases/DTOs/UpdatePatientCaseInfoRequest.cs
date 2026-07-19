using NPE.Core.Modules.Patients.Models;

namespace NPE.Core.Modules.Cases.Models;

public sealed class UpdatePatientCaseInfoRequest
{
    public PatientDTO Patient { get; set; }
        = new();

    public CaseDTO Case { get; set; }
        = new();
}