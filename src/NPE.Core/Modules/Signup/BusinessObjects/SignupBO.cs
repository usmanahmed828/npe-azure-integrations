using NPE.Core.Common.UnitOfWork;
using NPE.Core.Modules.Signup.Models;
using NPE.Core.Modules.Signup.Validators;

namespace NPE.Core.Modules.Signup.BusinessObjects
{
    public interface ISignupService
    {
        Task<SignupResponseDTO> SignupAsync(CompanySignupRequest request);
        //Task<SignupResponseDTO> SignupAsync(CompanySignupRequest request, CancellationToken cancellationToken = default);
    }

    public interface ISignupBO
    {
        Task<SignupResponseDTO> SignupAsync(CompanySignupRequest request);
    }

    public class SignupBO : ISignupBO
    {
        private readonly ISignupService _service;
        private readonly IUnitOfWork _uow;

        public SignupBO(ISignupService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public async Task<SignupResponseDTO> SignupAsync(CompanySignupRequest request)
        {
            SignupValidator.Validate(request);

            await _uow.BeginAsync();

            try
            {
                var result = await _service.SignupAsync(request);

                await _uow.CommitAsync();

                return result;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}