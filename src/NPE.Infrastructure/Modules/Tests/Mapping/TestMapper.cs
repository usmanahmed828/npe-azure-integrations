using NPE.Core.Modules.Tests.Models;
using NPE.Infrastructure.Modules.Tenancy.Entities;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Core.Modules.Tests.Mapping
{
    public static class TestMapper
    {
        public static TestDTO ToCore(this Test entity)
        {
            return new TestDTO
            {
                Id = entity.ID,
                Code = entity.Code,
                Name = entity.Name,
                ReportName = entity.ReportName,
                TestHeading = entity.TestHeading,
                ReportGroup = entity.ReportGroup,
                Synonyms = entity.Synonyms,
                GroupId = entity.GroupID,
                Type = entity.Type,
                IsSpecial = entity.IsSpecial,
                TestDays = entity.TestDays,
                ReportDays = entity.ReportDays,
                SpecimenId = entity.SpecimenID,
                SpecimenReqQuantity = entity.SpecimenReqQuantity,
                Comments = entity.Comments,
                SortOrder = entity.SortOrder,
                Rate = entity.Rate,
                Unit = entity.Unit,
                TemplateId = entity.TemplateID,
                Format = entity.Format,
                CriticalValueLowerBound = entity.CriticalValueLowerBound,
                CriticalValueUpperBound = entity.CriticalValueUpperBound,
                Status = entity.Status,
                TestType = entity.TestType,
                StabilityFrozen = entity.StabilityFrozen,
                StabilityRefrigerated = entity.StabilityRefrigerated,
                StabilityRoom = entity.StabilityRoom,
                InstrumentId = entity.InstrumentID,
                MultipleInstrument = entity.MultipleInstrument
            };
        }

        public static void UpdateEntity(this TestDTO core, Test entity)
        {
            entity.Code = core.Code;
            entity.Name = core.Name;
            entity.ReportName = core.ReportName;
            entity.TestHeading = core.TestHeading;
            entity.ReportGroup = core.ReportGroup;
            entity.Synonyms = core.Synonyms;
            entity.GroupID = core.GroupId;
            entity.Type = core.Type;
            entity.IsSpecial = core.IsSpecial;
            entity.TestDays = core.TestDays;
            entity.ReportDays = core.ReportDays;
            entity.SpecimenID = core.SpecimenId;
            entity.SpecimenReqQuantity = core.SpecimenReqQuantity;
            entity.Comments = core.Comments;
            entity.SortOrder = core.SortOrder;
            entity.Rate = core.Rate;
            entity.Unit = core.Unit;
            entity.TemplateID = core.TemplateId;
            entity.Format = core.Format;
            entity.CriticalValueLowerBound = core.CriticalValueLowerBound;
            entity.CriticalValueUpperBound = core.CriticalValueUpperBound;
            entity.Status = core.Status;
            entity.TestType = core.TestType;
            entity.StabilityFrozen = core.StabilityFrozen;
            entity.StabilityRefrigerated = core.StabilityRefrigerated;
            entity.StabilityRoom = core.StabilityRoom;
            entity.InstrumentID = core.InstrumentId;
            entity.MultipleInstrument = core.MultipleInstrument;
        }

        public static CompanyTest ToEntity(int companyId, int testId)
        {
            return new CompanyTest
            {
                CompanyId = companyId,
                TestId = testId
            };
        }
    }
}