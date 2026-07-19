using NPE.Core.Modules.Tests.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Tests.Models
{
    public class TestStructureDTO
    {
        public int TestId { get; set; }

        public byte Type { get; set; }

        public List<TestParameterDto>? Parameters { get; set; }

        public List<TestNormalValueDto>? NormalValues { get; set; }

        public List<int>? ChildTests { get; set; }
    }

    public static class TestTypes
    {
        public const byte Normal = 0;
        public const byte Parameterized = 1;
        public const byte Profile = 2;
        public const byte Package = 3;
    }

    //public class TestParameterDto
    //{
    //    public int Id { get; set; }   // 0 for new

    //    public string Name { get; set; } = "";
    //    public string ReportName { get; set; } = "";

    //    public string Format { get; set; } = "";

    //    public string? Unit { get; set; }

    //    public bool IsEditable { get; set; }
    //}

    //public class TestNormalValueDto
    //{
    //    public byte Gender { get; set; }

    //    public byte? FromAge { get; set; }
    //    public byte? ToAge { get; set; }

    //    public decimal? FromValue { get; set; }
    //    public decimal? ToValue { get; set; }

    //    public string? TextValue { get; set; }
    //}
}

namespace NPE.Core.Modules.Tests.DTOs
{
    public class TestListDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Rate { get; set; }

        public byte Type { get; set; }

        public bool IsSpecial { get; set; }

        public short GroupId { get; set; }

        public bool Status { get; set; }
    }
    public class TestDetailsDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ReportName { get; set; } = string.Empty;

        public string? Synonyms { get; set; }

        public short GroupId { get; set; }

        public byte Type { get; set; }

        public decimal Rate { get; set; }

        public bool IsSpecial { get; set; }

        public short TestType { get; set; }

        public bool Status { get; set; }
    }
    public class TestNormalValueDto
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

        public bool Status { get; set; }
    }
    public class TestParameterDto
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ReportName { get; set; } = string.Empty;

        public string Format { get; set; } = string.Empty;

        public string? Unit { get; set; }

        public bool IsEditable { get; set; }

        public bool Status { get; set; }
    }
    public class TestParameterNormalValueDto
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

        public bool Status { get; set; }
    }
    public class TestProfileDto
    {
        public int ProfileId { get; set; }

        public int TestId { get; set; }

        public short? SortOrder { get; set; }
    }
    public class TestGroupDto
    {
        public short Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ReportName { get; set; } = string.Empty;

        public short DepartmentId { get; set; }

        public short? SortOrder { get; set; }

        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }

        public bool Status { get; set; }
    }
    public class TestDepartmentDto
    {
        public short Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

        public bool Status { get; set; }
    }
    public class TestInstrumentDto
    {
        public long Id { get; set; }

        public int TestId { get; set; }

        public int InstrumentId { get; set; }

        public string InstrumentName { get; set; } = string.Empty;

        public bool Status { get; set; }
    }
    public class TestTemplateDto
    {
        public short Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public byte[]? Image { get; set; }

        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }

        public bool Status { get; set; }

        public string? Department { get; set; }

        public string? ReportName { get; set; }

        public int DoctorStampId { get; set; }

        public bool? NewReportFormatInd { get; set; }

        public DateTime? CutOffDate { get; set; }

        public string? TemplateGroupName { get; set; }


    }
}
