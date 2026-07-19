using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Tests.Models;

namespace NPE.Core.Modules.Tests.BusinessObjects
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetAllAsync();

        Task<TestDTO?> GetByIdAsync(int id);
        Task<TestDTO> CreateAsync(TestDTO test);
        Task<TestDTO> UpdateAsync(TestDTO test);

        Task CloneBaselineTestsAsync(int targetClientId);
    }

    public class TestBO
    {
        private readonly ITestService _testservice;
        private readonly ITestRateLookupService _rateLookupService;
        private readonly IUnitOfWork _uow;

        public TestBO(ITestService service, ITestRateLookupService rateLookupService, IUnitOfWork uow)
        {
            _testservice = service;
            _rateLookupService = rateLookupService;
            _uow = uow;
        }

        public Task<TestDTO?> GetByIdAsync(int id)   => _testservice.GetByIdAsync(id);
        public Task<IEnumerable<TestDTO>> GetAllAsync()  => _testservice.GetAllAsync();
        public async Task<TestDTO> CreateAsync(TestDTO model)
        {
            await _uow.BeginAsync();

            try
            {
                Validate(model);

                var result = await _testservice.CreateAsync(model);

                await _uow.CommitAsync();

                return result;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
        public async Task<TestDTO> UpdateAsync(TestDTO model)
        {
            await _uow.BeginAsync();

            try
            {
                Validate(model);

                var result = await _testservice.UpdateAsync(model);

                await _uow.CommitAsync();

                return result;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
 
        public async Task CloneBaselineTestsAsync(int targetClientId)
        {
            //await _uow.BeginAsync();

            try
            {
                await _testservice.CloneBaselineTestsAsync(targetClientId);

                //await _uow.CommitAsync();
            }
            catch
            {
                //await _uow.RollbackAsync();
                throw;
            }
        }


        public Task<List<TestRateLookupDto>> LoadRatesAsync(int referenceId,short rateTypeId,int gender,int centerId)
        {
            return _rateLookupService.LoadByReferenceAsync(referenceId,rateTypeId,gender,centerId);
        }

        private static void Validate(TestDTO model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.Code))
                errors.Add("Test code is required.");

            if (string.IsNullOrWhiteSpace(model.Name))
                errors.Add("Test name is required.");

            if (model.GroupId <= 0)
                errors.Add("Test group is required.");

            if (errors.Any())
                throw new Core.Common.Validation.ValidationException(errors);
        }
    }
}
