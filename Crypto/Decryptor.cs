using System;
using System.IO;
using System.Security.Cryptography;

namespace MFramework.Common.Core.Crypto
{
    public class Decryptor:IDecryptor
    {
        private ICryptoTransform _transform;

        public Decryptor(ICryptoTransform trasform)
        {
            _transform = trasform;
        }

        
        public CryptoStream Decrypt(Stream encryptedStream)
        {


            return new CryptoStream(encryptedStream, _transform, CryptoStreamMode.Read);

        }

        public byte[] Decrypt(byte[] data)
        {
            return Decrypt(data, data.Length);
        }

        public byte[] Decrypt(byte[] data, int length)
        {
            try
        {
            CryptoStream cs = Decrypt(new MemoryStream(data));

            // Create buffer to hold the decrypted data.
            byte[] result = new byte[length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            cs.Read(result, 0, result.Length);
                
            return result;
        }
        catch (CryptographicException ex)
        {
            return null;
        }
        
    }
    }
 }
    

