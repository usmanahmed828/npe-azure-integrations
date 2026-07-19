using NPE.Core.Modules.Tests.Models;
using NPE.Infrastructure.Modules.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Infrastructure.Modules.Tests.Mapping
{
    public static class TestDepartmentMapping
    {
        public static TestDepartmentCore ToCore(this TestDepartment entity)
        {
            return new TestDepartmentCore
            {
                Id = entity.ID,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status
            };
        }

        public static void UpdateEntity(this TestDepartmentCore core, TestDepartment entity)
        {
            entity.Name = core.Name;
            entity.Description = core.Description;
            entity.Status = core.Status;
        }
    }

    public static class TestGroupMapping
    {
        public static TestGroupCore ToCore(this TestGroup entity)
        {
            return new TestGroupCore
            {
                Id = entity.ID,
                Name = entity.Name,
                ReportName = entity.ReportName,
                DepartmentId = entity.DepartmentID,
                SortOrder = entity.SortOrder,
                Status = entity.Status ?? true
            };
        }

        public static void UpdateEntity(this TestGroupCore core, TestGroup entity)
        {
            entity.Name = core.Name;
            entity.ReportName = core.ReportName;
            entity.DepartmentID = core.DepartmentId;
            entity.SortOrder = core.SortOrder;
            entity.Status = core.Status;
        }
    }

    public static class TestMapping
    {
        public static TestDTO ToCore(this Test entity)
        {
            return new TestDTO
            {
                Id = entity.ID,
                Code = entity.Code,
                Name = entity.Name,
                ReportName = entity.ReportName,
                Synonyms = entity.Synonyms,

                GroupId = entity.GroupID,
                Type = entity.Type,
                TestType = entity.TestType,

                Rate = entity.Rate,
                Unit = entity.Unit,

                TestDays = entity.TestDays,
                ReportDays = entity.ReportDays,

                Status = entity.Status,
                IsSpecial = entity.IsSpecial,

                Comments = entity.Comments,
                SortOrder = entity.SortOrder
            };
        }

        public static void UpdateEntity(this TestDTO core, Test entity)
        {
            entity.Code = core.Code;
            entity.Name = core.Name;
            entity.ReportName = core.ReportName;
            entity.Synonyms = core.Synonyms;

            entity.GroupID = core.GroupId;
            entity.Type = core.Type;
            entity.TestType = core.TestType;

            entity.Rate = core.Rate;
            entity.Unit = core.Unit;

            entity.TestDays = core.TestDays;
            entity.ReportDays = core.ReportDays;

            entity.IsSpecial = core.IsSpecial;
            entity.Comments = core.Comments;
            entity.SortOrder = core.SortOrder;

            entity.Status = core.Status;
        }
    }

    public static class TestNormalValueMapping
    {
        public static TestNormalValueCore ToCore(this TestNormalValue entity)
        {
            return new TestNormalValueCore
            {
                Id = entity.ID,
                TestId = entity.TestID,
                Gender = entity.Gender,

                FromAge = entity.FromAge,
                ToAge = entity.ToAge,

                FromValue = entity.FromValue,
                ToValue = entity.ToValue,

                TextValue = entity.TextValue,
                Remarks = entity.Remarks,

                Status = entity.Status
            };
        }

        public static void UpdateEntity(this TestNormalValueCore core, TestNormalValue entity)
        {
            entity.Gender = core.Gender;
            entity.FromAge = core.FromAge;
            entity.ToAge = core.ToAge;

            entity.FromValue = core.FromValue;
            entity.ToValue = core.ToValue;

            entity.TextValue = core.TextValue;
            entity.Remarks = core.Remarks;

            entity.Status = core.Status;
        }
    }

    public static class TestParameterMapping
    {
        public static TestParameterCore ToCore(this TestParameter entity)
        {
            return new TestParameterCore
            {
                Id = entity.ID,
                TestId = entity.TestID,
                Name = entity.Name,
                ReportName = entity.ReportName,
                Unit = entity.Unit,
                Format = entity.Format,
                Status = entity.Status
            };
        }

        public static void UpdateEntity(this TestParameterCore core, TestParameter entity)
        {
            entity.Name = core.Name;
            entity.ReportName = core.ReportName;
            entity.Unit = core.Unit;
            entity.Format = core.Format;
            entity.Status = core.Status;
        }
    }

    public static class TestTemplateMapping
    {
        public static TestTemplateCore ToCore(this TestTemplate entity)
        {
            return new TestTemplateCore
            {
                Id = entity.ID,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status
            };
        }

        public static void UpdateEntity(this TestTemplateCore core, TestTemplate entity)
        {
            entity.Name = core.Name;
            entity.Description = core.Description;
            entity.Status = core.Status;
        }
    }
}
