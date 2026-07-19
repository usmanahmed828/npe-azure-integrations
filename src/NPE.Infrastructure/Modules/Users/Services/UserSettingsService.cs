using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.Users.BusinessObjects;
using NPE.Core.Modules.Users.DTOs;
using NPE.Infrastructure.Modules.Users.Entities;

namespace NPE.Infrastructure.Modules.Users.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly ApplicationDbContext _db;

        public UserSettingsService(
            ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserSettingsDTO>
            GetAsync(int userId)
        {
            var settings =
                await _db.Set<UserWebSettings>().Where(x => x.UserId == userId).ToListAsync();

            return new UserSettingsDTO
            {
                DefaultLocation =
                    GetValue(settings, 1),

                RowCount =
                    GetValue(settings, 2),

                DateRange =
                    GetValue(settings, 3),

                DefaultTestStatus =
                    GetValue(settings, 4),

                ShowHeaderFooter =
                    GetBool(settings, 5),

                DefaultReferenceID =
                    GetValue(settings, 6)
            };
        }

        private int GetValue(
            List<UserWebSettings> data,
            int settingId)
        {
            return data
                .FirstOrDefault(
                    x => x.SettingId == settingId)
                ?.Value ?? 0;
        }

        private bool GetBool(
            List<UserWebSettings> data,
            int settingId)
        {
            return GetValue(
                data,
                settingId) == 1;
        }
    }
}
