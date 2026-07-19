using Microsoft.EntityFrameworkCore;

using NPE.Core.Common.Security;

using NPE.Core.Modules.Auth.BusinessObjects;
using NPE.Core.Modules.Auth.DTOs;

namespace NPE.Infrastructure.Modules.Auth.Services
{
    public class InternalUserService
        : IInternalUserService
    {
        private readonly ApplicationDbContext
            _db;

        private readonly IPasswordEncryption
            _encryption;

        public InternalUserService(
            ApplicationDbContext db,
            IPasswordEncryption encryption)
        {
            _db =
                db;

            _encryption =
                encryption;
        }

        public InternalUserModel?
            ValidateUser(
                string username,
                string password)
        {
            var encryptedPassword =
                _encryption
                    .Encrypt(password);

            var user =
                _db.ILockUsers

                    .AsNoTracking()

                    .FirstOrDefault(x =>
                        x.UserName ==
                        username
                        &&
                        x.Password ==
                        encryptedPassword
                        &&
                        !x.Disabled);

            if (user == null)
            {
                return null;
            }

            var centerId =
    _db.UserWebSettings

        .AsNoTracking()

        .Where(x =>
            x.UserId ==
            user.UserId
            &&
            x.SettingId == 1)

        .Select(x =>
            x.Value)

        .FirstOrDefault();

            return new InternalUserModel
            {
                UserId =
                    user.UserId,

                CompanyId =
                    user.CompanyId,

                CenterId = centerId,

                Username =
                    user.UserName,

                FullName =
                    $"{user.FirstName} {user.LastName}",

                // TEMPORARY
                // Replace later with DB-driven permissions
                Permissions =
                    new List<string>
                    {
                        Permissions.Patients.Read,
                        Permissions.Patients.Create,
                        Permissions.Cases.Create
                    }
            };
        }
    }
}