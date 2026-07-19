using Microsoft.AspNetCore.Mvc;
using NPE.Core.Common.Crud;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace NPE.API.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<TDto, TKey> : ControllerBase
    {
        protected readonly ICrudService<TDto, TKey> Service;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IValidator<TDto>? Validator;

        protected BaseCrudController(ICrudService<TDto, TKey> service, IUnitOfWork unitOfWork, IValidator<TDto>? validator = null)
        {
            Service = service;
            UnitOfWork = unitOfWork;
            Validator = validator;
        }

        #region Query Operations

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(TKey id)
        {
            var result = await Service.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var result = await Service.GetAllAsync();

            return Ok(result);
        }

        #endregion

        #region Command Operations

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TDto dto)
        {
            Validator?.ValidateForCreate(dto);

            var result = await ExecuteTransactionAsync(async () => await Service.CreateAsync(dto));

            return Ok(result);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TDto dto)
        {
            Validator?.ValidateForUpdate(dto);

            await ExecuteTransactionAsync(async () => { await Service.UpdateAsync(dto); });

            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            await ExecuteTransactionAsync(async () => { await Service.DeleteAsync(id); });

            return Ok();
        }

        #endregion

        #region Transaction Helpers

        protected async Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action)
        {
            await UnitOfWork.BeginAsync();

            try
            {
                var result = await action();

                await UnitOfWork.CommitAsync();

                return result;
            }
            catch
            {
                await UnitOfWork.RollbackAsync();
                throw;
            }
        }

        protected async Task ExecuteTransactionAsync(Func<Task> action)
        {
            await UnitOfWork.BeginAsync();

            try
            {
                await action();

                await UnitOfWork.CommitAsync();
            }
            catch
            {
                await UnitOfWork.RollbackAsync();
                throw;
            }
        }

        #endregion
    }
}