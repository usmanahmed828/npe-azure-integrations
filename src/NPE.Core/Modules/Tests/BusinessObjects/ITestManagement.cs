using NPE.Core.Modules.Tests.DTOs;
using NPE.Core.Modules.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Tests.BusinessObjects
{
    public interface ITestDepartmentService
    {
        Task<List<TestDepartmentDto>> GetAllAsync();
        Task<TestDepartmentDto?> GetByIdAsync(short id);
        Task<TestDepartmentDto> SaveAsync(TestDepartmentDto model);
    }

    public interface ITestGroupService
    {
        Task<List<TestGroupDto>> GetByDepartmentAsync(short departmentId);
        Task<TestGroupDto?> GetByIdAsync(short id);
        Task<TestGroupDto> SaveAsync(TestGroupDto model);
    }

    //public interface ITestService
    //{
    //    Task<List<TestLookupDto>> SearchAsync(string? search);
    //    Task<TestListDto?> GetByIdAsync(int id);
    //    Task<TestListDto> SaveAsync(TestListDto model);
    //}

    public interface ITestNormalValueService
    {
        Task<List<TestNormalValueDto>> GetByTestAsync(int testId);
        Task<TestNormalValueDto> SaveAsync(TestNormalValueDto values);
    }

    public interface ITestParameterService
    {
        Task<List<TestParameterDto>> GetByTestAsync(int testId);
        Task<TestParameterDto> SaveAsync(TestParameterDto parameters);
    }

    public interface ITestProfileService
    {
        Task<List<TestProfileDto>> GetByProfileAsync(int profileId);
        Task SaveAsync(int profileId, List<int> testIds);
    }

    public interface ITestTemplateService
    {
        Task<List<TestTemplateDto>> GetAllAsync();
        Task<TestTemplateDto> SaveAsync(TestTemplateDto model);
    }
}
