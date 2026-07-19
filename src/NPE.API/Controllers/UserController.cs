using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.Users;
using NPE.Core.Modules.Users.BusinessObjects;
using NPE.Core.Modules.Users.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController
        : ControllerBase
    {
        private readonly IUserBO
            _bo;

        public UsersController(
            IUserBO bo)
        {
            _bo =
                bo;
        }

        #region Create Admin User

        /// <summary>
        /// Creates a new administrator user.
        /// Used primarily during tenant onboarding.
        /// </summary>
        [HttpPost("admin")]
        [SwaggerRequestExample(typeof(CreateAdminUserRequest), typeof(CreateAdminUserExample))]
        public async Task<IActionResult>
            CreateAdminUser(
                [FromBody]
                CreateAdminUserRequest request)
        {
            var userId =
                await _bo
                    .CreateAdminUserAsync(
                        request);

            return Ok(
                new
                {
                    UserId =
                        userId,

                    Message =
                        "Administrator user created successfully."
                });
        }

        #endregion
    }
}
