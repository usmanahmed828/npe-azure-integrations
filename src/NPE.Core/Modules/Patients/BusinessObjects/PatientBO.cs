using NPE.Core.Common;
using NPE.Core.Common.Crud;
using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Patients.Models;
using NPE.Core.Modules.Patients.Validators;

namespace NPE.Core.Modules.Patients.BusinessObjects
{
    public interface IPatientService : ICrudService<PatientDTO, long>
    {
        //Task<PatientDTO> CreateAndReturnAsync(PatientDTO dto);

        Task<PagedResult<PatientSearchDTO>> SearchAsync(
            PatientSearchRequest request);

        Task<PagedResult<SearchPatientListDTO>>
            SearchPatientListAsync(
                CaseSearchParamsDto dto);
    }
    public interface IPatientBO
    {
        Task<PatientDTO?> GetByIdAsync(long id);
        Task<IEnumerable<PatientDTO>> GetAllAsync();

        Task<long> CreateAsync(PatientDTO dto);

        Task<PatientDTO> CreateAndReturnAsync(PatientDTO dto);

        Task UpdateAsync(PatientDTO dto);
        Task DeleteAsync(long id);

        //Task<List<PatientSearchResult>> SearchAsync(
        //    PatientSearchRequest request);

        Task<PagedResult<PatientSearchDTO>> SearchAsync(PatientSearchRequest request);

        Task<PagedResult<SearchPatientListDTO>>
            SearchPatientListAsync(
                CaseSearchParamsDto dto);
    }
    public class PatientBO : IPatientBO
    {
        private readonly IPatientService _service;
        private readonly IUnitOfWork _uow;

        public PatientBO(
            IPatientService service,
            IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        #region CRUD

        public Task<PatientDTO?> GetByIdAsync(long id)
            => _service.GetByIdAsync(id);

        public Task<IEnumerable<PatientDTO>> GetAllAsync()
            => _service.GetAllAsync();

        public async Task<long> CreateAsync(PatientDTO dto)
        {
            PatientValidator.Validate(dto);

            await _uow.BeginAsync();

            try
            {
                var id = await _service.CreateAsync(dto);
                await _uow.CommitAsync();
                return id;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task<PatientDTO> CreateAndReturnAsync(PatientDTO dto)
        {
            PatientValidator.Validate(dto);

            await _uow.BeginAsync();

            try
            {
                var patient = await _service.CreateAndReturnAsync(dto);

                await _uow.CommitAsync();

                return patient;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(PatientDTO dto)
        {
            PatientValidator.Validate(dto);

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

        public async Task DeleteAsync(long id)
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

        //public Task<List<PatientSearchResult>> SearchAsync(
        //    PatientSearchRequest request)
        //    => _service.SearchAsync(request);
        public async Task<PagedResult<PatientSearchDTO>> SearchAsync(PatientSearchRequest request)
        {
            return await _service.SearchAsync(request);
        }

        public Task<PagedResult<SearchPatientListDTO>> SearchPatientListAsync(CaseSearchParamsDto dto)
        {
            if (dto.Page <= 0) dto.Page = 1;
            if (dto.PageSize <= 0) dto.PageSize = 25;

            return _service.SearchPatientListAsync(dto);
        }
    }
}