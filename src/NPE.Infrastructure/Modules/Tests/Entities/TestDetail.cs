using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestDetail
{
    public int ID { get; set; }

    public int? BOMID { get; set; }

    public string? Description { get; set; }

    public virtual Test IDNavigation { get; set; } = null!;
}
