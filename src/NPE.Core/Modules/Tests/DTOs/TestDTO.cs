namespace NPE.Core.Modules.Tests.Models
{
    public class TestDTO
    {
        public int Id { get; set; }

        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string ReportName { get; set; } = "";

        public string? TestHeading { get; set; }
        public string? ReportGroup { get; set; }
        public string? Synonyms { get; set; }

        public short GroupId { get; set; }
        public byte Type { get; set; }
        public bool IsSpecial { get; set; }

        public byte? TestDays { get; set; }
        public byte? ReportDays { get; set; }

        public short? SpecimenId { get; set; }
        public string? SpecimenReqQuantity { get; set; }

        public string? Comments { get; set; }
        public int? SortOrder { get; set; }

        public decimal Rate { get; set; }
        public string? Unit { get; set; }

        public short? TemplateId { get; set; }
        public string? Format { get; set; }

        public decimal? CriticalValueLowerBound { get; set; }
        public decimal? CriticalValueUpperBound { get; set; }

        public bool Status { get; set; }
        public short TestType { get; set; }

        public string? StabilityFrozen { get; set; }
        public string? StabilityRefrigerated { get; set; }
        public string? StabilityRoom { get; set; }

        public int? InstrumentId { get; set; }
        public bool? MultipleInstrument { get; set; }
    }

    public class TestDepartmentCore
    {
        public short Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; }
    }

    public class TestGroupCore
    {
        public short Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ReportName { get; set; } = string.Empty;

        public short DepartmentId { get; set; }

        public short? SortOrder { get; set; }

        public bool Status { get; set; }
    }

    //public class TestCore
    //{
    //    public int Id { get; set; }

    //    public string Code { get; set; } = string.Empty;

    //    public string Name { get; set; } = string.Empty;

    //    public string ReportName { get; set; } = string.Empty;

    //    public string? TestHeading { get; set; }

    //    public string? ReportGroup { get; set; }

    //    public string? Synonyms { get; set; }

    //    public short GroupId { get; set; }

    //    public byte Type { get; set; }

    //    public bool IsSpecial { get; set; }

    //    public byte? TestDays { get; set; }

    //    public byte? ReportDays { get; set; }

    //    public short? SpecimenId { get; set; }

    //    public string? SpecimenReqQuantity { get; set; }

    //    public string? Comments { get; set; }

    //    public int? SortOrder { get; set; }

    //    public decimal Rate { get; set; }

    //    public string? Unit { get; set; }

    //    public short? TemplateId { get; set; }

    //    public string? Format { get; set; }

    //    public decimal? CriticalValueLowerBound { get; set; }

    //    public decimal? CriticalValueUpperBound { get; set; }

    //    public bool Status { get; set; }

    //    public short TestType { get; set; }

    //    public string? StabilityFrozen { get; set; }

    //    public string? StabilityRefrigerated { get; set; }

    //    public string? StabilityRoom { get; set; }

    //    public int? InstrumentId { get; set; }

    //    public bool? MultipleInstrument { get; set; }
    //}

    public class TestDetailCore
    {
        public int Id { get; set; }

        public int? BomId { get; set; }

        public string? Description { get; set; }
    }

    public class TestParameterCore
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ReportName { get; set; } = string.Empty;

        public string? Unit { get; set; }

        public short TemplateId { get; set; }

        public bool IsEditable { get; set; }

        public int? SortOrder { get; set; }

        public string? Description { get; set; }

        public string Format { get; set; } = string.Empty;

        public decimal? CriticalValueLowerBound { get; set; }

        public decimal? CriticalValueUpperBound { get; set; }

        public bool Status { get; set; }

        public bool? CalculatedValue { get; set; }

        public string? CalculatedValueFormula { get; set; }

        public string? ParameterName { get; set; }
    }

    public class TestNormalValueCore
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public byte Gender { get; set; }

        public byte? FromAge { get; set; }

        public byte? ToAge { get; set; }

        public decimal? FromValue { get; set; }

        public decimal? ToValue { get; set; }

        public string? TextValue { get; set; }

        public string? Remarks { get; set; }

        public string? AgeType { get; set; }

        public int? InstrumentId { get; set; }

        public int? CenterId { get; set; }

        public bool Status { get; set; }
    }

    public class TestParameterNormalValueCore
    {
        public int Id { get; set; }

        public int TestParameterId { get; set; }

        public byte Gender { get; set; }

        public byte? FromAge { get; set; }

        public byte? ToAge { get; set; }

        public decimal? FromValue { get; set; }

        public decimal? ToValue { get; set; }

        public string? TextValue { get; set; }

        public string? Remarks { get; set; }

        public string? AgeType { get; set; }

        public int? InstrumentId { get; set; }

        public int? CenterId { get; set; }

        public bool Status { get; set; }
    }

    public class TestProfileCore
    {
        public int ProfileId { get; set; }

        public int TestId { get; set; }

        public short? SortOrder { get; set; }
    }

    public class TestInstrumentCore
    {
        public long Id { get; set; }

        public int TestId { get; set; }

        public int CenterId { get; set; }

        public int InstrumentId { get; set; }

        public string InstrumentName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; }

        public bool? IsAllowedSave { get; set; }
    }

    public class TestSettingCore
    {
        public int TestId { get; set; }

        public bool? AllowSelection { get; set; }

        public string? DefaultValue { get; set; }

        public string? SelectionList { get; set; }

        public int? Duration { get; set; }

        public byte? Gender { get; set; }

        public bool? AdditionalInfo { get; set; }

        public string? AdditionalInfoValidationFields { get; set; }

        public bool? IsHide { get; set; }

        public bool? IsAllowDiscount { get; set; }

        public string? Settings { get; set; }
    }

    public class TestTemplateCore
    {
        public short Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public byte[]? Image { get; set; }

        public bool Status { get; set; }

        public string? Department { get; set; }

        public string? ReportName { get; set; }

        public int DoctorStampId { get; set; }

        public bool? NewReportFormatInd { get; set; }

        public DateTime? CutOffDate { get; set; }

        public string? TemplateGroupName { get; set; }
    }
}