using System.Security.Cryptography;

namespace MFramework.Common.Core.Crypto.AES
{
    public class AesEncryptionProvider:IEncryptionProvider<AesEncryptionProvider.Key>
    {

        private AesCryptoServiceProvider _aesProvider;

        public AesEncryptionProvider()
        {
            _aesProvider= new AesCryptoServiceProvider();
        }
        public IEncryptor GetEncryptor(Key key)
        {
            return new Encryptor(_aesProvider.CreateEncryptor(key.Key,key.Vector));
        }

        public IDecryptor GetDecryptor(Key key)
        {
            return new Decryptor(_aesProvider.CreateDecryptor(key.Key, key.Vector)); ;
        }
        public class Key : EncryptionKeyBase
        {
            public Key(string key, string vector)
                : base(key, vector)
            {
            }

            public Key(byte[] key, byte[] vector)
                : base(key, vector)
            {
            }

            protected override int KeyLength
            {
                get { return 32; }
            }

            protected override int VectorLength
            {
                get { return 16; }
            }
        }
    }
}
