using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.iLock.BusinessObjects;
using NPE.Core.Modules.iLock.DTOs;
using NPE.Infrastructure.Modules.iLock.Builders;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.iLock.Models;

namespace NPE.Infrastructure.Modules.iLock.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly MenuBuilder _builder;

        public MenuService(ApplicationDbContext context, MenuBuilder builder)
        {
            _context = context;
            _builder = builder;
        }

        public async Task<List<MenuNodeDTO>> GetUserMenuAsync(int companyId, int userId)
        {
            var menuData = await _context
                .Set<ILockGroupUser>()
                .Where(x => x.CompanyId == companyId && x.UserId == userId)
                .SelectMany(x => x.ILockGroup.Permissions)
                .Where(x => x.UIObject.UIContainerId == 7)
                .Select(x => new MenuFlatItem
                {
                    Id = x.UIObject.UIObjectId,
                    Key = x.UIObject.Name,
                    DisplayName = x.UIObject.DisplayName,
                    Description = x.UIObject.Description
                })
                .Distinct()
                .ToListAsync();

            return _builder.Build(menuData);
        }
    }
}
