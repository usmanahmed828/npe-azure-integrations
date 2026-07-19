using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Security;
using NPE.Core.Modules.Auth.Authorization;
using NPE.Core.Modules.Auth.BusinessObjects;
using NPE.Core.Modules.Auth.DTOs;

namespace NPE.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBO _authBO;

        public AuthController(IAuthBO authBO)
        {
            _authBO = authBO;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] InternalLoginRequest request)
        {
            var response = _authBO.AuthenticateInternal(request);

            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }

        [HttpPost("external")]
        public IActionResult External([FromBody] ExternalAppRequest request)
        {
            var token = _authBO.AuthenticateExternal(request);

            if (token == null)
                return Unauthorized("Invalid external app credentials");

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("bootstrap")]
        public async Task<IActionResult> Bootstrap()
        {
            var response = await _authBO.GetBootstrapAsync();

            return Ok(response);
        }

        [RequiresPermission(Permissions.Patients.Read)]
        [HttpGet("me")]
        public IActionResult Me(
    [FromServices] ICurrentUser currentUser)
        {
            return Ok(new
            {
                currentUser.UserId,
                currentUser.CompanyId,
                currentUser.Username,
                currentUser.IsAuthenticated
            });
        }
    }
}