using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestTemplate
{
    public short ID { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    public string? Department { get; set; }

    public string? ReportName { get; set; }

    public int DoctorStempId { get; set; }

    public bool? NewReportFormatInd { get; set; }

    public DateTime? CutOffDate { get; set; }

    public string? TemplateGroupName { get; set; }
}
