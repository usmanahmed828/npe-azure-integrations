namespace NPE.Core.Common.Security
{
    public interface IPasswordEncryption
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}