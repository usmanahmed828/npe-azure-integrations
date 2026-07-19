using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Context.Services;
using NPE.Core.Common.Security;
using NPE.Core.Modules.Bootstrap.DTOs;
using NPE.Core.Modules.Bootstrap.Services;
using NPE.Core.Modules.iLock.BusinessObjects;
using NPE.Core.Modules.Lookups.Services;
using NPE.Core.Modules.Management.Centers.Services;
using NPE.Core.Modules.Users.BusinessObjects;
using NPE.Infrastructure.Modules.iLock.Mapping;

namespace NPE.Infrastructure.Modules.Bootstrap.Services
{
    public class BootstrapService : IBootstrapService
    {
        private readonly ICurrentContextService _currentContextService;
        private readonly ILookupResolverService _lookupResolverService;
        private readonly ICenterService _centerService;
        private readonly IUserSettingsService _userSettingsService;
        private readonly IMenuService _menuService;
        private readonly ICurrentUser _currentUser;
        private readonly ApplicationDbContext _context;

        public BootstrapService(ICurrentContextService currentContextService, ILookupResolverService lookupResolverService, ICenterService centerService, IUserSettingsService userSettingsService,
            IMenuService menuService, ICurrentUser currentUser, ApplicationDbContext context)
        {
            _currentContextService = currentContextService;
            _lookupResolverService = lookupResolverService;
            _centerService = centerService;
            _userSettingsService = userSettingsService;
            _menuService = menuService;
            _currentUser = currentUser;
            _context = context;
        }

        public async Task<BootstrapResponseDTO> GetAsync()
        {
            var context = await _currentContextService.GetAsync();

            #region User

            var user = await _context.ILockUsers.AsNoTracking().FirstAsync(x => x.UserId == context.UserId);

            #endregion

            #region Menus

            var menu = await _menuService.GetUserMenuAsync(context.CompanyId, context.UserId);
            var sidebar = MenuMapper.ToSidebar(menu);

            #endregion

            #region User Settings

            var userSettings = await _userSettingsService.GetAsync(context.UserId);

            #endregion

            #region Lookups

            var lookupBundle = await _centerService.GetLookupAsync();

            #endregion

            #region Lookups

            var consultants = await _lookupResolverService.GetConsultantsAsync();

            var references = await _lookupResolverService.GetReferencesAsync();

            #endregion

            return new BootstrapResponseDTO
            {
                Security = new BootstrapSecurityDTO
                {
                    User = new UserInfoDTO
                    {
                        UserId = user.UserId,
                        CompanyId = context.CompanyId,
                        CenterId = context.CenterId,
                        Username = user.UserName,
                        FullName = $"{user.FirstName} {user.LastName}"
                    },

                    Permissions = new List<string> { Permissions.Patients.Read, Permissions.Patients.Create, Permissions.Cases.Create },
                    Modules = new List<string> { "patients", "cases", "reports" },
                    UserSettings = userSettings,
                    Menus = sidebar
                },

                Operational = new BootstrapOperationalDTO
                {
                    Context = new BootstrapContextDTO
                    {
                        CompanyId = context.CompanyId,
                        CenterId = context.CenterId,
                        RootCenterId = context.RootCenterId,
                        UserId = context.UserId,
                        UserName = context.UserName,
                        IsPureSaaS = context.IsPureSaaS
                    },

                    Lookups = new BootstrapLookupsDTO
                    {
                        RegistrationCenters = lookupBundle.RegistrationLocations,
                        DestinationCenters = lookupBundle.DestinationLocations,
                        Consultants = consultants,
                        References = references
                    }
                }
            };
        }
    }
}