using NPE.Core.Common.Identity;
using NPE.Core.Common.Security;
using NPE.Core.Modules.Users.BusinessObjects;
using NPE.Core.Modules.Users.DTOs;
using NPE.Infrastructure.Modules.iLock.Entities;
using NPE.Infrastructure.Modules.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace NPE.Infrastructure.Modules.Users.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IPasswordEncryption _passwordEncryption;

        public UserService(ApplicationDbContext db, IIdentityService identityService, IPasswordEncryption passwordEncryption)
        {
            _context = db;
            _identityService = identityService;
            _passwordEncryption = passwordEncryption;
        }

        public async Task<int> CreateAdminUserAsync(CreateAdminUserRequest request)
        {
            await ValidateAsync(request);

            var userId = await _identityService.ConsumeIlockAsync(IdentityTypes.User);
            var names = request.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = names.FirstOrDefault() ?? "";
            var lastName = names.Length > 1 ? string.Join(" ", names.Skip(1)) : "";
            var encryptedPassword = _passwordEncryption.Encrypt(request.Password);

            #region User

            var user = new ILockUser
            {
                CompanyId = request.CompanyId,
                UserId = userId,
                UserName = request.Username,
                FirstName = firstName,
                LastName = lastName,
                Email = request.Email,
                Mobile = request.Phone,
                Password = encryptedPassword,
                Disabled = false,
                CreatedBy = "SYSTEM",
                CreatedDate = DateTime.Now
            };

            _context.ILockUsers.Add(user);

            #endregion

            #region User Location

            _context.ILockUserLocations.Add(new ILockUserLocation
            {
                CompanyId = request.CompanyId,
                UserId = userId,
                LocationId = request.RootCenterId.ToString(),
                CreatedBy = "SYSTEM",
                CreatedDate = DateTime.Now
            });

            #endregion

            #region Administrator Group

            //_context.ILockGroupUsers.Add(new ILockGroupUser
            //{
            //    CompanyId = request.CompanyId,
            //    UserId = userId,
            //    GroupId = 1,
            //    CreatedBy = "SYSTEM",
            //    CreatedDate = DateTime.Now
            //});
            await AssignUserToAllGroupsAsync(request.CompanyId, userId);

            #endregion

            #region User Settings

            _context.UserWebSettings.AddRange(BuildDefaultSettings(userId, request.RootCenterId, request.DefaultReferenceId));

            #endregion

            return userId;
        }

        #region Validation

        private async Task ValidateAsync(CreateAdminUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidOperationException("Username is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new InvalidOperationException("Password is required.");
            }

            var exists = await _context.ILockUsers.AnyAsync(x => x.CompanyId == request.CompanyId && x.UserName == request.Username);

            if (exists)
            {
                throw new InvalidOperationException($"User '{request.Username}' already exists.");
            }
        }

        #endregion

        #region Helpers

        private static List<UserWebSettings> BuildDefaultSettings(int userId, int rootCenterId, int defaultReferenceId)
        {
            return
            [
                new UserWebSettings
                {
                    UserId = userId,
                    SettingId = 1,
                    Value = rootCenterId
                },

                new UserWebSettings
                {
                    UserId = userId,
                    SettingId = 2,
                    Value = 25
                },

                new UserWebSettings
                {
                    UserId = userId,
                    SettingId = 3,
                    Value = 7
                },

                new UserWebSettings
                {
                    UserId = userId,
                    SettingId = 4,
                    Value = 0
                },

                new UserWebSettings
                {
                    UserId = userId,
                    SettingId = 5,
                    Value = 1
                },

                new UserWebSettings
                {
                    UserId = userId,
                    SettingId = 6,
                    Value = defaultReferenceId
                }
            ];
        }

        private async Task AssignUserToAllGroupsAsync(int companyId, int userId)
        {
            var groups = await _context.ILockGroups
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .ToListAsync();

            foreach (var group in groups)
            {
                _context.ILockGroupUsers.Add(new ILockGroupUser
                {
                    CompanyId = companyId,
                    UserId = userId,
                    GroupId = group.GroupId,
                    CreatedBy = "SYSTEM",
                    CreatedDate = DateTime.Now
                });
            }
        }

        #endregion
    }
}