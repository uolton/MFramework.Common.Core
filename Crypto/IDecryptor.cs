using System.IO;
using System.Security.Cryptography;

namespace MFramework.Common.Core.Crypto
{
    public interface IDecryptor
    {
        CryptoStream Decrypt(Stream encryptedStream);
        byte[] Decrypt(byte[] data);
        byte[] Decrypt(byte[] data, int length);
    }
}
