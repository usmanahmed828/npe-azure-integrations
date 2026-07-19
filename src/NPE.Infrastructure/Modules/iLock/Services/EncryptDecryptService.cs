using Microsoft.EntityFrameworkCore;
using NPE.Core.Modules.iLock.BusinessObjects;
using NPE.Core.Modules.iLock.DTOs;
using NPE.Infrastructure.Modules.iLock.Entities;
using System.Security.Cryptography;
using System.Text;

namespace NPE.Infrastructure.Modules.iLock.Services
{
    public class EncryptDecryptService : IEncryptDecryptService
    {
        private readonly ApplicationDbContext _context;

        public EncryptDecryptService(ApplicationDbContext context)
        {
            _context = context;
        }

        private static readonly byte[] EncriptionIV = { 233, 202, 193, 161, 235, 246, 7, 139 };
        private static readonly byte[] EncriptionKey = {
            6, 110, 50, 140, 227, 156, 42, 141, 233, 178, 23, 225,
            42, 222, 201, 183, 250, 4, 26, 32, 97, 7, 204, 153
        };

        public async Task<string> EncryptString(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using TripleDES tripleDes = TripleDES.Create();
            tripleDes.Key = EncriptionKey;
            tripleDes.IV = EncriptionIV;
            tripleDes.Mode = CipherMode.CBC; // Default for TripleDES
            tripleDes.Padding = PaddingMode.PKCS7;

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using var transform = tripleDes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            return await Task.FromResult(Convert.ToBase64String(result));
        }
        public async Task<string> DecryptString(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            using TripleDES tripleDes = TripleDES.Create();
            tripleDes.Key = EncriptionKey;
            tripleDes.IV = EncriptionIV;

            byte[] inputBytes = Convert.FromBase64String(input);
            using var transform = tripleDes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            return await Task.FromResult(Encoding.UTF8.GetString(result));
        }

    }
}
