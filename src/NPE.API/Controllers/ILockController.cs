using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPE.Core.Modules.iLock.BusinessObjects;

namespace NPE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ILockController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IIlockService _ilockService;

        public ILockController(IMenuService menuService, IIlockService ilockService)
        {
            _menuService = menuService;
            _ilockService = ilockService;
        }

        [HttpGet("signin")]
        public async Task<IActionResult> SignInIlockUser(string username, string password)
        {
            var result = await _ilockService.SignInIlockUser(username, password);
            return Ok(result);
        }

        [HttpGet("menu")]
        public async Task<IActionResult> GetMenu(int companyId, int userId)
        {
            var result = await _menuService.GetUserMenuAsync(companyId, userId);
            return Ok(result);
        }
    }
}
