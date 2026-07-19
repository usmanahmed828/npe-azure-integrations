using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestDepartment
{
    public short ID { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<TestGroup> TestGroups { get; set; } = new List<TestGroup>();
}
