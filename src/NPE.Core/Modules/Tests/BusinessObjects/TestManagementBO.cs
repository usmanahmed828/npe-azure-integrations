using NPE.Core.Modules.Tests.DTOs;
using NPE.Core.Modules.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPE.Core.Modules.Tests.BusinessObjects
{
    public class TestManagementBO
    {
        private readonly ITestDepartmentService _departmentService;
        private readonly ITestGroupService _groupService;
        private readonly ITestService _testService;
        private readonly ITestNormalValueService _normalValueService;
        private readonly ITestParameterService _parameterService;
        private readonly ITestProfileService _profileService;
        private readonly ITestTemplateService _templateService;

        public TestManagementBO(
            ITestDepartmentService departmentService,
            ITestGroupService groupService,
            ITestService testService,
            ITestNormalValueService normalValueService,
            ITestParameterService parameterService,
            ITestProfileService profileService,
            ITestTemplateService templateService)
        {
            _departmentService = departmentService;
            _groupService = groupService;
            _testService = testService;
            _normalValueService = normalValueService;
            _parameterService = parameterService;
            _profileService = profileService;
            _templateService = templateService;
        }

        #region Departments

        public Task<List<TestDepartmentDto>> GetDepartmentsAsync()
            => _departmentService.GetAllAsync();

        public Task<TestDepartmentDto> SaveDepartmentAsync(TestDepartmentDto model)
            => _departmentService.SaveAsync(model);

        #endregion

        #region Groups

        public Task<List<TestGroupDto>> GetGroupsAsync(short departmentId)
            => _groupService.GetByDepartmentAsync(departmentId);

        public Task<TestGroupDto> SaveGroupAsync(TestGroupDto model)
            => _groupService.SaveAsync(model);

        #endregion

        #region Normal Values

        public Task<List<TestNormalValueDto>> GetNormalValuesAsync(int testId)
            => _normalValueService.GetByTestAsync(testId);

        public Task SaveNormalValuesAsync(TestNormalValueDto values)
            => _normalValueService.SaveAsync(values);

        #endregion

        #region Parameters

        public Task<List<TestParameterDto>> GetParametersAsync(int testId)
            => _parameterService.GetByTestAsync(testId);

        public Task SaveParametersAsync(TestParameterDto parameters)
            => _parameterService.SaveAsync(parameters);

        #endregion

        #region Profiles

        public Task<List<TestProfileDto>> GetProfileTestsAsync(int profileId)
            => _profileService.GetByProfileAsync(profileId);

        public Task SaveProfileAsync(int profileId, List<int> tests)
            => _profileService.SaveAsync(profileId, tests);

        #endregion

        #region Templates

        public Task<List<TestTemplateDto>> GetTemplatesAsync()
            => _templateService.GetAllAsync();

        public Task<TestTemplateDto> SaveTemplateAsync(TestTemplateDto model)
            => _templateService.SaveAsync(model);

        #endregion
    }

    public class TestDepartmentBO
    {
        private readonly ITestDepartmentService _service;

        public TestDepartmentBO(ITestDepartmentService service)
        {
            _service = service;
        }

        public Task<List<TestDepartmentDto>> GetAllAsync()
            => _service.GetAllAsync();

        public Task<TestDepartmentDto?> GetByIdAsync(short id)
            => _service.GetByIdAsync(id);

        public Task<TestDepartmentDto> SaveAsync(TestDepartmentDto dto)
            => _service.SaveAsync(dto);
    }

    public class TestGroupBO
    {
        private readonly ITestGroupService _service;

        public TestGroupBO(ITestGroupService service)
        {
            _service = service;
        }

        public Task<List<TestGroupDto>> GetByDepartmentAsync(short departmentId)
            => _service.GetByDepartmentAsync(departmentId);

        public Task<TestGroupDto?> GetByIdAsync(short id)
            => _service.GetByIdAsync(id);

        public Task<TestGroupDto> SaveAsync(TestGroupDto dto)
            => _service.SaveAsync(dto);
    }

    public class TestNormalValueBO
    {
        private readonly ITestNormalValueService _service;

        public TestNormalValueBO(ITestNormalValueService service)
        {
            _service = service;
        }

        public Task<List<TestNormalValueDto>> GetByTestAsync(int testId)
            => _service.GetByTestAsync(testId);

        public Task<TestNormalValueDto> SaveAsync(TestNormalValueDto dto)
            => _service.SaveAsync(dto);
    }

    public class TestParameterBO
    {
        private readonly ITestParameterService _service;

        public TestParameterBO(ITestParameterService service)
        {
            _service = service;
        }

        public Task<List<TestParameterDto>> GetByTestAsync(int testId)
            => _service.GetByTestAsync(testId);

        public Task<TestParameterDto> SaveAsync(TestParameterDto dto)
            => _service.SaveAsync(dto);
    }
}
