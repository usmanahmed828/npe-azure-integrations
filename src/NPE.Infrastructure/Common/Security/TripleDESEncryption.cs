using NPE.Core.Common.Security;
using System.Security.Cryptography;
using System.Text;

namespace NPE.Infrastructure.Common.Security
{
    public class TripleDESEncryption
        : IPasswordEncryption
    {
        private static readonly byte[] _iv =
        {
            233, 202, 193, 161,
            235, 246, 7, 139
        };

        private static readonly byte[] _key =
        {
            6, 110, 50, 140,
            227, 156, 42, 141,
            233, 178, 23, 225,
            42, 222, 201, 183,
            250, 4, 26, 32,
            97, 7, 204, 153
        };

        public string Encrypt(string value)
        {
            var inputBytes =
                Encoding.UTF8.GetBytes(value);

            using var memory =
                new MemoryStream();

            using var provider =
                new TripleDESCryptoServiceProvider();

            using var cryptoStream =
                new CryptoStream(
                    memory,
                    provider.CreateEncryptor(
                        _key,
                        _iv),
                    CryptoStreamMode.Write);

            cryptoStream.Write(
                inputBytes,
                0,
                inputBytes.Length);

            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(
                memory.ToArray());
        }

        public string Decrypt(string value)
        {
            var inputBytes =
                Convert.FromBase64String(value);

            using var memory =
                new MemoryStream();

            using var provider =
                new TripleDESCryptoServiceProvider();

            using var cryptoStream =
                new CryptoStream(
                    memory,
                    provider.CreateDecryptor(
                        _key,
                        _iv),
                    CryptoStreamMode.Write);

            cryptoStream.Write(
                inputBytes,
                0,
                inputBytes.Length);

            cryptoStream.FlushFinalBlock();

            return Encoding.UTF8.GetString(
                memory.ToArray());
        }
    }
}