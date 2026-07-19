using Microsoft.AspNetCore.Mvc;
using NPE.Core.Modules.Cases.BusinessObjects;
using NPE.Core.Modules.Cases.DTOs;
using NPE.Core.Modules.Cases.Models;
using Swashbuckle.AspNetCore.Filters;
using NPE.API.SwaggerExamples.Cases;

namespace NPE.API.Controllers;

[ApiController]
[Route("api/cases")]
public sealed class CasesController
    : ControllerBase
{
    private readonly ICaseBO _bo;

    public CasesController(
        ICaseBO bo)
    {
        _bo = bo;
    }

    #region Helpers

    [HttpPost("case-number")]
    public async Task<IActionResult>
        GetNextCaseNumber(
        [FromBody]
        GetNextCaseNumberRequest dto)
    {
        var number =
            await _bo.GetNextCaseNumberAsync(
                dto.Location,
                dto.CaseRegDate);

        return Ok(new
        {
            CaseNumber = number
        });
    }

    #endregion

    #region Registration
    [HttpGet("{id:long}")]
    public async Task<IActionResult>
    GetById(
    long id)
    {
        var result =
            await _bo.GetByIdAsync(id);

        if (result == null)
            return NotFound(
                "Case not found.");

        return Ok(result);
    }

    //[HttpGet]
    //public async Task<IActionResult>
    //    GetAll()
    //{
    //    var result =
    //        await _bo.GetAllAsync();

    //    return Ok(result);
    //}

    /// <summary>
    /// Save new case for existing patient.
    /// </summary>
    [HttpPost]
    [SwaggerRequestExample(typeof(CaseDTO), typeof(CreateCaseExample))]
    public async Task<IActionResult>
        Create(
        [FromBody]
        CaseDTO dto)
    {
        var id =
            await _bo.CreateAsync(dto);

        return Ok(new
        {
            CaseId = id
        });
    }

    #endregion

    #region Full Edit

    /// <summary>
    /// Full case update.
    /// Parent + children.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult>
        Update(
        [FromBody]
        CaseDTO dto)
    {
        await _bo.UpdateAsync(dto);

        return Ok(
            "Case updated successfully.");
    }

    #endregion

    #region Add New Children

    /// <summary>
    /// Add tests / remarks /
    /// clinical findings / payment
    /// to existing case.
    /// </summary>
    [HttpPost("{id}/test-in-case")]
    public async Task<IActionResult>
        AddTestInCase(
        long id,
        [FromBody]
        AddCaseChildrenRequest request)
    {
        request.CaseId = id;

        var result =
            await _bo.AddTestInCaseAsync(
                request);

        return Ok(result);
    }

    #endregion

    #region Parent Only Update

    /// <summary>
    /// Update patient parent
    /// and case parent only.
    /// </summary>
    [HttpPut("patient-case-info")]
    public async Task<IActionResult>
        UpdateParentInfo(
        [FromBody]
        UpdatePatientCaseInfoRequest request)
    {
        var result =
            await _bo.UpdatePatientCaseInfoAsync(
                request);

        return Ok(result);
    }

    #endregion

    #region Soft Delete Later

    // NexusPro uses Status=false.
    // Keep disabled until approved.

    #endregion
}