using NPE.Core.Common.Responses;
using NPE.Core.Common.Security;
using NPE.Core.Modules.Auth.DTOs;
using NPE.Core.Modules.Bootstrap.DTOs;
using NPE.Core.Modules.Bootstrap.Services;

namespace NPE.Core.Modules.Auth.BusinessObjects
{
    public interface IAuthBO
    {
        ApiResponse<InternalLoginResponse> AuthenticateInternal(InternalLoginRequest request);
        string? AuthenticateExternal(ExternalAppRequest request);
        Task<ApiResponse<BootstrapResponseDTO>> GetBootstrapAsync();
    }
    public interface IJwtService
    {
        string GenerateInternalToken(InternalUserModel user);
        string GenerateExternalToken(ExternalAppModel app);
    }
    public interface IExternalAppService
    {
        ExternalAppModel? ValidateAndGetApp(string appId, string sharedSecret);
    }
    public interface IInternalUserService
    {
        InternalUserModel? ValidateUser(string username, string password);
        //Task<BootstrapResponse> GetBootstrapAsync(int userId, int companyId);
    }

    public class AuthBO : IAuthBO
    {
        private readonly IJwtService _jwtService;
        private readonly IExternalAppService _externalAppService;
        private readonly IInternalUserService _internalUserService;
        private readonly ICurrentUser _currentUser;
        private readonly IBootstrapService _bootstrapService;

        public AuthBO(
            IJwtService jwtService,
            IExternalAppService externalAppService,
            IInternalUserService internalUserService,
            ICurrentUser currentUser,
            IBootstrapService bootstrapService)
        {
            _jwtService = jwtService;
            _externalAppService = externalAppService;
            _internalUserService = internalUserService;
            _currentUser = currentUser;
            _bootstrapService = bootstrapService;
        }

        public ApiResponse<InternalLoginResponse> AuthenticateInternal(InternalLoginRequest request)
        {
            var user = _internalUserService.ValidateUser(request.Username, request.Password);

            if (user == null)
            {
                return ApiResponse<InternalLoginResponse>
                    .FailResponse(
                        new List<string>
                        {
                    "Invalid username or password"
                        });
            }

            var token =
                _jwtService.GenerateInternalToken(user);

            return ApiResponse<InternalLoginResponse>
                .SuccessResponse(
                    new InternalLoginResponse
                    {
                        Token = token,

                        UserId = user.UserId,

                        CompanyId = user.CompanyId,

                        Username = user.Username,

                        FullName = user.FullName
                    });
        }

        public string? AuthenticateExternal(ExternalAppRequest request)
        {
            var app = _externalAppService.ValidateAndGetApp(
                request.AppId,
                request.SharedSecret);

            if (app == null)
                return null;

            return _jwtService.GenerateExternalToken(app);
        }

        public async Task<ApiResponse<BootstrapResponseDTO>> GetBootstrapAsync()
        {
            //var result = await _internalUserService.GetBootstrapAsync(_currentUser.UserId, _currentUser.CompanyId);
            var result = await _bootstrapService.GetAsync();

            return ApiResponse<BootstrapResponseDTO>.SuccessResponse(result);
        }
    }
}
