using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestProfile
{
    public int ProfileID { get; set; }

    public int TestID { get; set; }

    public short? SortOrder { get; set; }

    public virtual Test Profile { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
