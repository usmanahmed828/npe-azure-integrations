using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.iLock.BusinessObjects;
using NPE.Core.Modules.iLock.DTOs;
using NPE.Infrastructure.Modules.iLock.Entities;

namespace NPE.Infrastructure.Modules.iLock.Services
{
    public class ILockService : IIlockService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptDecryptService _encryptionService;

        public ILockService(ApplicationDbContext context, IEncryptDecryptService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public async Task<IlockUserDTO> SignInIlockUser(string username, string password)
        {
            string encryptedPassword = await _encryptionService.EncryptString(password);
            var userEntity = await _context.ILockUsers.FirstOrDefaultAsync(u => u.UserName == username && u.Password == encryptedPassword);

            if (userEntity == null)
            {
                throw new Exception("Invalid Username or Password.");
            }

            // 3. Check if account is disabled
            if (userEntity.Disabled)
            {
                throw new Exception("Your account is disabled.");
            }

            // 4. Map Entity to DTO (Manual mapping for simplicity)
            return new IlockUserDTO
            {
                Username = userEntity.UserName,
                Name = userEntity.FirstName + " " + userEntity.LastName
            };
        }
    }
}
