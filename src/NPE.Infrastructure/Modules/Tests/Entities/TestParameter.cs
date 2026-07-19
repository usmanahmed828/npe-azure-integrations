using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestParameter
{
    public int ID { get; set; }

    public int TestID { get; set; }

    public string Name { get; set; } = null!;

    public string ReportName { get; set; } = null!;

    public string? Unit { get; set; }

    public short TemplateID { get; set; }

    public bool IsEditable { get; set; }

    public int? SortOrder { get; set; }

    public string? Description { get; set; }

    public string Format { get; set; } = null!;

    public decimal? CriticalValueLowerBound { get; set; }

    public decimal? CriticalValueUpperBound { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    public bool? CalculatedValue { get; set; }

    public string? CalculatedValueFormula { get; set; }

    public string? ParameterName { get; set; }

    public virtual Test Test { get; set; } = null!;

    public virtual ICollection<TestParameterNormalValue> TestParameterNormalValues { get; set; } = new List<TestParameterNormalValue>();
}
