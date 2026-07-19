using Microsoft.AspNetCore.Mvc;
using NPE.API.SwaggerExamples.Signup;
using NPE.Core.Modules.Signup.BusinessObjects;
using NPE.Core.Modules.Signup.Models;
using Swashbuckle.AspNetCore.Filters;

namespace NPE.API.Controllers
{
    [ApiController]
    [Route("api/signup")]
    public class SignupController : ControllerBase
    {
        private readonly ISignupBO _bo;

        public SignupController(ISignupBO bo)
        {
            _bo = bo;
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(CompanySignupRequest), typeof(CreateCompanySignupExample))]
        public async Task<IActionResult> Signup([FromBody] CompanySignupRequest request)
        {
            return Ok(await _bo.SignupAsync(request));
        }
    }
}