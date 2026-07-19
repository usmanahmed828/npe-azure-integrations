using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestNormalValueGraph
{
    public int ID { get; set; }

    public int TestNormalValueID { get; set; }

    public int TestParameterNormalValueID { get; set; }

    public string StatusName { get; set; } = null!;

    public string? StatusNameLabel { get; set; }

    public string? FromValue { get; set; }

    public string? ToValue { get; set; }

    public string? Color { get; set; }

    public byte? SortOrder { get; set; }

    public bool Status { get; set; }
}
