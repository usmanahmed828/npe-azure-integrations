using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class LISSpecimenType
{
    public int ID { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public bool? AllowPrintName { get; set; }

    public bool? Status { get; set; }
}
