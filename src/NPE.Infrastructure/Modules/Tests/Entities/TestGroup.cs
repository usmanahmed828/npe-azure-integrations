using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestGroup
{
    public short ID { get; set; }

    public string Name { get; set; } = null!;

    public string ReportName { get; set; } = null!;

    public short DepartmentID { get; set; }

    public short? SortOrder { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? Status { get; set; }

    public virtual TestDepartment Department { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
