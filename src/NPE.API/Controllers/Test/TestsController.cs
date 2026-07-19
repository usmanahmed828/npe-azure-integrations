using Microsoft.AspNetCore.Mvc;
using NPE.Core.Common.Security;
using NPE.Core.Modules.Auth.Authorization;
using NPE.Core.Modules.Tests.BusinessObjects;
using NPE.Core.Modules.Tests.DTOs;
using NPE.Core.Modules.Tests.Models;

namespace NPE.API.Controllers.Test
{
    [ApiController]
    [Route("api/tests")]
    public class TestsController : ControllerBase
    {
        private readonly TestManagementBO _testManagementBO;
        private readonly TestBO _testBO;


        public TestsController(TestManagementBO testManagementbo, TestBO testBO)
        {
            _testManagementBO = testManagementbo;
            _testBO = testBO;
        }

        #region Test
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _testBO.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var test = await _testBO.GetByIdAsync(id);
            return test == null ? NotFound() : Ok(test);
        }

        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestDTO model)  => Ok(await _testBO.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TestDTO model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            return Ok(await _testBO.UpdateAsync(model));
        }


        [HttpPost("clone-baseline")]
        public async Task<IActionResult> CloneBaselineTests([FromQuery] int targetClientId = 0)
        {
            if (targetClientId <= 0) return BadRequest("Target Client ID must be provided.");

            await _testBO.CloneBaselineTestsAsync(targetClientId);

            return Ok(new { Message = $"BOOM! All Status 0 tests from Client {1001} cloned to Client {targetClientId} successfully." });
        }


       

        #endregion

        #region Departments

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
            => Ok(await _testManagementBO.GetDepartmentsAsync());

        [HttpPost("departments")]
        public async Task<IActionResult> SaveDepartment(TestDepartmentDto model)
            => Ok(await _testManagementBO.SaveDepartmentAsync(model));

        #endregion

        #region Groups

        [HttpGet("groups/{departmentId:int}")]
        public async Task<IActionResult> GetGroups(short departmentId)
            => Ok(await _testManagementBO.GetGroupsAsync(departmentId));

        [HttpPost("groups")]
        public async Task<IActionResult> SaveGroup(TestGroupDto model)
            => Ok(await _testManagementBO.SaveGroupAsync(model));

        #endregion

        #region Normal Values

        [HttpGet("{testId:int}/normal-values")]
        public async Task<IActionResult> GetNormalValues(int testId)
            => Ok(await _testManagementBO.GetNormalValuesAsync(testId));

        [HttpPost("normal-values")]
        public async Task<IActionResult> SaveNormalValues(TestNormalValueDto values)
        {
            await _testManagementBO.SaveNormalValuesAsync(values);
            return Ok();
        }

        #endregion

        #region Parameters

        [HttpGet("{testId:int}/parameters")]
        public async Task<IActionResult> GetParameters(int testId)
            => Ok(await _testManagementBO.GetParametersAsync(testId));

        [HttpPost("parameters")]
        public async Task<IActionResult> SaveParameters(TestParameterDto parameters)
        {
            await _testManagementBO.SaveParametersAsync(parameters);
            return Ok();
        }

        #endregion

        #region Profiles

        [HttpGet("{profileId:int}/profile-tests")]
        public async Task<IActionResult> GetProfileTests(int profileId)
            => Ok(await _testManagementBO.GetProfileTestsAsync(profileId));

        [HttpPost("{profileId:int}/profile-tests")]
        public async Task<IActionResult> SaveProfile(int profileId, List<int> tests)
        {
            await _testManagementBO.SaveProfileAsync(profileId, tests);
            return Ok();
        }

        #endregion

        #region Templates

        [HttpGet("templates")]
        public async Task<IActionResult> GetTemplates()
            => Ok(await _testManagementBO.GetTemplatesAsync());

        [HttpPost("templates")]
        public async Task<IActionResult> SaveTemplate(TestTemplateDto model)
            => Ok(await _testManagementBO.SaveTemplateAsync(model));

        #endregion

        #region RATE LOOKUP

        [HttpGet("rates")]
        public async Task<IActionResult> LoadRates(
            int referenceId,
            short rateTypeId,
            int gender,
            int centerId
            )
        {
            var results = await _testBO.LoadRatesAsync(
                referenceId,
                rateTypeId,
                gender,
                centerId
                );

            return Ok(results);
        }

        #endregion
    }
}