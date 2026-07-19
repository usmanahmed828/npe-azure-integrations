using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Tests.BusinessObjects;
using NPE.Core.Modules.Tests.DTOs;
using NPE.Infrastructure.Modules.Tests.Entities;

namespace NPE.Infrastructure.Modules.Tests.Services
{
    public class TestDepartmentService : ITestDepartmentService
    {
        private readonly ApplicationDbContext _db;

        public TestDepartmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestDepartmentDto>> GetAllAsync()
        {
            return await _db.TestDepartments
                .AsNoTracking()
                .Select(d => new TestDepartmentDto
                {
                    Id = d.ID,
                    Name = d.Name,
                    Description = d.Description,
                    CreatedBy = d.CreatedBy,
                    CreatedDate = d.CreatedDate,
                    ModifiedBy = d.ModifiedBy,
                    ModifiedDate = d.ModifiedDate,
                    Status = d.Status
                })
                .ToListAsync();
        }

        public async Task<TestDepartmentDto?> GetByIdAsync(short id)
        {
            return await _db.TestDepartments
                .Where(x => x.ID == id)
                .Select(d => new TestDepartmentDto
                {
                    Id = d.ID,
                    Name = d.Name,
                    Description = d.Description,
                    CreatedBy = d.CreatedBy,
                    CreatedDate = d.CreatedDate,
                    ModifiedBy = d.ModifiedBy,
                    ModifiedDate = d.ModifiedDate,
                    Status = d.Status
                    
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TestDepartmentDto> SaveAsync(TestDepartmentDto model)
        {
            TestDepartment entity;

            if (model.Id == 0)
            {
                entity = new TestDepartment();
                _db.TestDepartments.Add(entity);
            }
            else
            {
                entity = await _db.TestDepartments.FindAsync(model.Id)
                         ?? throw new Exception("Department not found");
            }

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.CreatedBy = model.CreatedBy;
            entity.CreatedDate = model.CreatedDate;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = model.ModifiedDate;
            entity.Status = model.Status;

            await _db.SaveChangesAsync();

            model.Id = entity.ID;
            return model;
        }
    }

    public class TestGroupService : ITestGroupService
    {
        private readonly ApplicationDbContext _db;

        public TestGroupService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestGroupDto>> GetByDepartmentAsync(short departmentId)
        {
            return await _db.TestGroups
                .Where(x => x.DepartmentID == departmentId)
                .Select(g => new TestGroupDto
                {
                    Id = g.ID,
                    Name = g.Name,
                    ReportName = g.ReportName,
                    DepartmentId = g.DepartmentID,
                    SortOrder = g.SortOrder,
                    CreatedBy = g.CreatedBy,
                    CreatedDate = g.CreatedDate,
                    ModifiedBy = g.ModifiedBy,
                    ModifiedDate = g.ModifiedDate,
                    Status = g.Status ?? false
                })
                .ToListAsync();
        }

        public async Task<TestGroupDto?> GetByIdAsync(short id)
        {
            return await _db.TestGroups
                .Where(x => x.ID == id)
                .Select(g => new TestGroupDto
                {
                    Id = g.ID,
                    Name = g.Name,
                    ReportName = g.ReportName,
                    DepartmentId = g.DepartmentID,
                    Status = g.Status ?? false
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TestGroupDto> SaveAsync(TestGroupDto model)
        {
            TestGroup entity;

            if (model.Id == 0)
            {
                entity = new TestGroup();
                _db.TestGroups.Add(entity);
            }
            else
            {
                entity = await _db.TestGroups.FindAsync(model.Id)
                         ?? throw new Exception("Group not found");
            }

            entity.Name = model.Name;
            entity.ReportName = model.ReportName;
            entity.DepartmentID = model.DepartmentId;
            entity.Status = model.Status;

            await _db.SaveChangesAsync();

            model.Id = entity.ID;
            return model;
        }
    }

    public class TestNormalValueService : ITestNormalValueService
    {
        private readonly ApplicationDbContext _db;

        public TestNormalValueService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestNormalValueDto>> GetByTestAsync(int testId)
        {
            return await _db.TestNormalValues
                .Where(x => x.TestID == testId)
                .Select(n => new TestNormalValueDto
                {
                    Id = n.ID,
                    TestId = n.TestID,
                    Gender = n.Gender,
                    FromAge = n.FromAge,
                    ToAge = n.ToAge,
                    FromValue = n.FromValue,
                    ToValue = n.ToValue,
                    TextValue = n.TextValue,
                    Remarks = n.Remarks,
                    Status = n.Status
                })
                .ToListAsync();
        }

        public async Task<TestNormalValueDto> SaveAsync(TestNormalValueDto model)
        {
            TestNormalValue entity;

            if (model.Id == 0)
            {
                entity = new TestNormalValue
                {
                    TestID = model.TestId
                };

                _db.TestNormalValues.Add(entity);
            }
            else
            {
                entity = await _db.TestNormalValues.FindAsync(model.Id)
                         ?? throw new Exception("Normal value not found");
            }

            entity.Gender = model.Gender;
            entity.FromAge = model.FromAge;
            entity.ToAge = model.ToAge;
            entity.FromValue = model.FromValue;
            entity.ToValue = model.ToValue;
            entity.TextValue = model.TextValue;
            entity.Remarks = model.Remarks;
            entity.Status = model.Status;

            await _db.SaveChangesAsync();

            model.Id = entity.ID;
            return model;
        }
    }

    public class TestParameterService : ITestParameterService
    {
        private readonly ApplicationDbContext _db;

        public TestParameterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestParameterDto>> GetByTestAsync(int testId)
        {
            return await _db.TestParameters
                .Where(x => x.TestID == testId)
                .Select(p => new TestParameterDto
                {
                    Id = p.ID,
                    TestId = p.TestID,
                    Name = p.Name,
                    ReportName = p.ReportName,
                    Format = p.Format,
                    Unit = p.Unit,
                    IsEditable = p.IsEditable,
                    Status = p.Status
                })
                .ToListAsync();
        }

        public async Task<TestParameterDto> SaveAsync(TestParameterDto model)
        {
            TestParameter entity;

            if (model.Id == 0)
            {
                entity = new TestParameter
                {
                    TestID = model.TestId
                };

                _db.TestParameters.Add(entity);
            }
            else
            {
                entity = await _db.TestParameters.FindAsync(model.Id)
                         ?? throw new Exception("Parameter not found");
            }

            entity.Name = model.Name;
            entity.ReportName = model.ReportName;
            entity.Format = model.Format;
            entity.Unit = model.Unit;
            entity.IsEditable = model.IsEditable;
            entity.Status = model.Status;

            await _db.SaveChangesAsync();

            model.Id = entity.ID;
            return model;
        }
    }

    public class TestProfileService : ITestProfileService
    {
        private readonly ApplicationDbContext _db;

        public TestProfileService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestProfileDto>> GetByProfileAsync(int profileId)
        {
            return await _db.TestProfiles
                .Where(x => x.ProfileID == profileId)
                .Select(p => new TestProfileDto
                {
                    ProfileId = p.ProfileID,
                    TestId = p.TestID,
                    SortOrder = p.SortOrder
                })
                .ToListAsync();
        }

        public async Task SaveAsync(int profileId, List<int> testIds)
        {
            var existing = await _db.TestProfiles
                .Where(x => x.ProfileID == profileId)
                .ToListAsync();

            _db.TestProfiles.RemoveRange(existing);

            var newItems = testIds.Select((t, index) => new TestProfile
            {
                ProfileID = profileId,
                TestID = t,
                SortOrder = (short)index
            });

            await _db.TestProfiles.AddRangeAsync(newItems);

            await _db.SaveChangesAsync();
        }
    }

    public class TestTemplateService : ITestTemplateService
    {
        private readonly ApplicationDbContext _db;

        public TestTemplateService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<TestTemplateDto>> GetAllAsync()
        {
            return await _db.TestTemplates
                .Select(t => new TestTemplateDto
                {
                    Id = t.ID,
                    Name = t.Name,
                    Description = t.Description,
                    Image = t.Image,
                    CreatedBy = t.CreatedBy,
                    CreatedDate = t.CreatedDate,    
                    ModifiedBy = t.ModifiedBy,
                    ModifiedDate = t.ModifiedDate,
                    Status = t.Status,
                    Department = t.Department,
                    ReportName = t.ReportName,
                    DoctorStampId = t.DoctorStempId,
                    NewReportFormatInd = t.NewReportFormatInd,
                    CutOffDate = t.CutOffDate,
                    TemplateGroupName = t.TemplateGroupName,

                })
                .ToListAsync();
        }

        public async Task<TestTemplateDto> SaveAsync(TestTemplateDto model)
        {
            TestTemplate entity;

            if (model.Id == 0)
            {
                entity = new TestTemplate();
                _db.TestTemplates.Add(entity);
            }
            else
            {
                entity = await _db.TestTemplates.FindAsync(model.Id)
                         ?? throw new Exception("Template not found");
            }

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Status = model.Status;

            await _db.SaveChangesAsync();

            model.Id = entity.ID;
            return model;
        }
    }
}
