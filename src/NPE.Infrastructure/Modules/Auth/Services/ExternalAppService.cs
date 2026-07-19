using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Auth.BusinessObjects;
using NPE.Core.Modules.Auth.DTOs;

namespace NPE.Infrastructure.Modules.Auth.Services
{
    public class ExternalAppService : IExternalAppService
    {
        private readonly ApplicationDbContext _db;

        public ExternalAppService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ExternalAppModel? ValidateAndGetApp(string appId, string sharedSecret)
        {
            var app = _db.ExternalApps
                .Include(x => x.Permissions)
                .FirstOrDefault(x =>
                    x.AppId == appId &&
                    x.SharedSecret == sharedSecret &&
                    x.IsActive);

            if (app == null)
                return null;

            return new ExternalAppModel
            {
                AppId = app.AppId,
                Permissions = app.Permissions
                                 .Select(p => p.Permission)
                                 .ToList()
            };
        }
    }
}
