using System.IO;
using System.Security.Cryptography;

namespace MFramework.Common.Core.Crypto
{
    public class Encryptor:IEncryptor
    {
        private ICryptoTransform _transform;
        private CryptoStream _stream;
        public Encryptor(ICryptoTransform trasform)
        {
            _transform = trasform;
        }

        public CryptoStream Encrypt(Stream toStream)
        {

            _stream = new CryptoStream(toStream, _transform, CryptoStreamMode.Write);
            return _stream;

        }

        public byte[] Encrypt(byte[] data)
        {
            return Encrypt(data, data.Length);
        }

        public byte[] Encrypt(byte[] data, int length)
        {
            try
            {
                // Create a MemoryStream.
                var ms = new MemoryStream();

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                var cs = Encrypt(ms);

                // Write the byte array to the crypto stream and flush it.
                cs.Write(data, 0, length);
                cs.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = ms.ToArray();

                // DoClose the streams.
                cs.Close();
                ms.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException ex)
            {
                return null;
            }
            return null;
        }

        public IEncryptor Flush()
        {
            _stream.FlushFinalBlock();
            return this;
        }
    }
    
}
