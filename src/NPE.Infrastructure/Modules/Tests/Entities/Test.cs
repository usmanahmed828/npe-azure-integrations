using System;
using System.Collections.Generic;

namespace NPE.Infrastructure.Modules.Tests.Entities;

public partial class Test
{
    public int ID { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ReportName { get; set; } = null!;

    public string? TestHeading { get; set; }

    public string? ReportGroup { get; set; }

    public string? Synonyms { get; set; }

    public short GroupID { get; set; }

    /// <summary>
    /// 0 For Normal , 1 for Profile and 2 for Package
    /// </summary>
    public byte Type { get; set; }

    public bool IsSpecial { get; set; }

    public byte? TestDays { get; set; }

    public byte? ReportDays { get; set; }

    public short? SpecimenID { get; set; }

    public string? SpecimenReqQuantity { get; set; }

    public string? Comments { get; set; }

    public int? SortOrder { get; set; }

    public decimal Rate { get; set; }

    public string? Unit { get; set; }

    public short? TemplateID { get; set; }

    public string? Format { get; set; }

    public decimal? CriticalValueLowerBound { get; set; }

    public decimal? CriticalValueUpperBound { get; set; }

    public string Createdby { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

    /// <summary>
    /// use to save testtype (0 THEN &apos;Routine&apos; WHEN 1 THEN &apos;Special&apos; WHEN 2 THEN &apos;PCR&apos; WHEN 3 THEN &apos;BIOPSY&apos; )
    /// </summary>
    public short TestType { get; set; }

    public string? StabilityFrozen { get; set; }

    public string? StabilityRefrigerated { get; set; }

    public string? StabilityRoom { get; set; }

    public int? InstrumentID { get; set; }

    public bool? MultipleInstrument { get; set; }

    public int? ClientID { get; set; }

    public virtual TestGroup Group { get; set; } = null!;

    public virtual TestDetail? TestDetail { get; set; }

    public virtual TestSetting? TestSetting { get; set; }

    public virtual ICollection<TestNormalValue> TestNormalValues { get; set; } = new List<TestNormalValue>();
    public virtual ICollection<TestParameter> TestParameters { get; set; } = new List<TestParameter>();
    public virtual ICollection<TestProfile> TestProfile { get; set; } = new List<TestProfile>();

    public virtual ICollection<TestInstrumentSetting> TestInstrumentSettings { get; set; } = new List<TestInstrumentSetting>();
    public virtual ICollection<LISSpecimenSetting> LISSpecimenSettings { get; set; } = new List<LISSpecimenSetting>();

}
