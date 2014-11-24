using System.IO;
using System.Security.Cryptography;

namespace MFramework.Common.Core.Crypto
{
    public interface IEncryptor
    {
        
        
        CryptoStream Encrypt(Stream toStream);
        byte[] Encrypt(byte[] data);
        byte[] Encrypt(byte[] data, int length);





    }
}
