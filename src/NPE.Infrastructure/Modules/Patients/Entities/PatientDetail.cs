using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Patients.Entities;

public partial class PatientDetail
{
    public long PatientId { get; set; }

    public string? PassportId { get; set; }

    public string? JobType { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? AgencyNo { get; set; }

    public string? Field4 { get; set; }

    public string? Field3 { get; set; }

    // Navigation property to Patient
    public virtual Patient Patient { get; set; } = null!;
}
