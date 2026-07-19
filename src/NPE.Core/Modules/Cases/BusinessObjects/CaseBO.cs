using NPE.Core.Common.CaseNumbers;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using NPE.Core.Modules.Cases.DTOs;
using NPE.Core.Modules.Cases.Models;

namespace NPE.Core.Modules.Cases.BusinessObjects;

#region Contracts

public interface ICaseService
{
    Task<long> CreateAsync(CaseDTO dto);
    Task UpdateAsync(CaseDTO dto);
    Task DeleteAsync(long id);
    Task<CaseDTO?> GetByIdAsync(long id);
    //Task<IEnumerable<CaseDTO>> GetAllAsync();
    Task<string> GetNextCaseNumberAsync(int location, DateTime date);

    Task<PatientCaseCreateResult> CreateWithPatientAsync(PatientCaseCreateDTO dto);
    Task<SaveCaseResult> AddTestInCaseAsync(AddCaseChildrenRequest request);
    Task<SaveCaseResult> UpdatePatientCaseInfoAsync(UpdatePatientCaseInfoRequest request);
    Task ReceiveDueAsync(long caseId, decimal receivedAmount, PaymentMethod method, string? cno = null, string? description = null, string? modifiedBy = null);
}

public interface ICaseBO
{
    #region CRUD

    Task<CaseDTO?> GetByIdAsync(long id);

    //Task<IEnumerable<CaseDTO>> GetAllAsync();

    Task<long> CreateAsync(CaseDTO dto);

    Task UpdateAsync(CaseDTO dto);

    Task DeleteAsync(long id);

    #endregion

    #region Helpers

    Task<string> GetNextCaseNumberAsync(
        int location,
        DateTime date);

    #endregion

    #region Workflows

    Task<SaveCaseResult> AddTestInCaseAsync(
        AddCaseChildrenRequest request);

    Task<SaveCaseResult> UpdatePatientCaseInfoAsync(
        UpdatePatientCaseInfoRequest request);

    Task ReceiveDueAsync(long caseId, decimal receivedAmount, PaymentMethod method, string? cno = null, string? description = null, string? modifiedBy = null);

    #endregion
}

#endregion

public sealed class CaseBO : ICaseBO
{
    private readonly ICaseService _service;
    private readonly ICaseNumberService _caseNumberService;
    private readonly IUnitOfWork _uow;

    public CaseBO(
        ICaseService service,
        ICaseNumberService caseNumberService,
        IUnitOfWork uow)
    {
        _service = service;
        _caseNumberService = caseNumberService;
        _uow = uow;
    }

    #region CRUD

    public Task<CaseDTO?> GetByIdAsync(
        long id)
        => _service.GetByIdAsync(id);

    //public Task<IEnumerable<CaseDTO>> GetAllAsync()
    //    => _service.GetAllAsync();

    public async Task<long> CreateAsync(
        CaseDTO dto)
    {
        CaseValidator.ValidateCreate(dto);

        await _uow.BeginAsync();

        try
        {
            var id =
                await _service.CreateAsync(dto);

            await _uow.CommitAsync();

            return id;
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(
        CaseDTO dto)
    {
        CaseValidator.ValidateUpdate(dto);

        await _uow.BeginAsync();

        try
        {
            await _service.UpdateAsync(dto);

            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(
        long id)
    {
        await _uow.BeginAsync();

        try
        {
            await _service.DeleteAsync(id);

            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    #endregion

    #region Helpers

    public Task<string> GetNextCaseNumberAsync(
        int location,
        DateTime date)
        => _caseNumberService
            .GetNextCaseNumberAsync(
                location,
                date);

    #endregion

    #region Workflows

    public Task<SaveCaseResult>
        AddTestInCaseAsync(
        AddCaseChildrenRequest request)
        => ExecuteAsync(
            () => _service
                .AddTestInCaseAsync(
                    request));

    public Task<SaveCaseResult>
        UpdatePatientCaseInfoAsync(
        UpdatePatientCaseInfoRequest request)
        => ExecuteAsync(
            () => _service
                .UpdatePatientCaseInfoAsync(
                    request));

    public async Task ReceiveDueAsync(long caseId, decimal receivedAmount, PaymentMethod method, string? cno = null, string? description = null, string? modifiedBy = null)
    {
        await _uow.BeginAsync();

        try
        {
            await _service.ReceiveDueAsync(caseId, receivedAmount, method, cno, description, modifiedBy);

            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    #endregion

    #region Private

    private async Task<SaveCaseResult>
        ExecuteAsync(
        Func<Task<SaveCaseResult>> action)
    {
        await _uow.BeginAsync();

        try
        {
            var result =
                await action();

            await _uow.CommitAsync();

            return result;
        }
        catch (ValidationException ex)
        {
            await _uow.RollbackAsync();

            return SaveCaseResult.Fail(
                ex.Errors);
        }
        catch (Exception ex)
        {
            await _uow.RollbackAsync();

            return SaveCaseResult.Fail(
                new[]
                {
                    ex.Message
                });
        }
    }

    #endregion
}