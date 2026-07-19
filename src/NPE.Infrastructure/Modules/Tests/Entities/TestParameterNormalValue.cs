using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestParameterNormalValue
{
    public int ID { get; set; }

    public int TestParameterID { get; set; }

    /// <summary>
    /// 0 for Male , 1 for Female and 2 for Both
    /// </summary>
    public byte Gender { get; set; }

    public byte? FromAge { get; set; }

    public byte? ToAge { get; set; }

    public decimal? FromValue { get; set; }

    public decimal? ToValue { get; set; }

    public string? TextValue { get; set; }

    public string? Remarks { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    public string? AgeType { get; set; }

    public int? InstrumentID { get; set; }

    public int? CenterID { get; set; }

    public virtual TestParameter TestParameter { get; set; } = null!;

    public virtual ICollection<TestNormalValueGraph> TestNormalValueGraph { get; set; } = new List<TestNormalValueGraph>();
}
