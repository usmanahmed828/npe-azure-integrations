using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class TestInstrumentSetting
{
    public long ID { get; set; }

    public int TestID { get; set; }

    public string TestCode { get; set; } = null!;

    public int CenterID { get; set; }

    public int InstrumentID { get; set; }

    public string InstrumentName { get; set; } = null!;

    public string? Description { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Status { get; set; }

    public bool? IsAllowedSave { get; set; }

    public virtual Test Test { get; set; } = null!;
}
