using System.IO;

namespace KTranslate.Infrastructure.Encryption
{
    public interface IEncryptionService
    {
        byte[] Encrypt(Stream toEncrypt, string password);

        string Decrypt(Stream encryptedData, string password);
    }
}
