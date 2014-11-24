using System.Linq;
using System.Text;

namespace MFramework.Common.Core.Crypto
{
    public abstract class EncryptionKeyBase:IEncryptionKey
    {
        private byte[] _key;
        private byte[] _vector;

        protected EncryptionKeyBase(string key, string vector)
            : this(
                ASCIIEncoding.ASCII.GetBytes(key),
                ASCIIEncoding.ASCII.GetBytes(vector)){}

        public EncryptionKeyBase(byte[] key, byte[] vector)
        {
            _key = key.Take(KeyLength).ToArray();
            _vector = vector.Take(VectorLength).ToArray(); ;
        }
        protected abstract int KeyLength { get; }
        protected abstract int VectorLength { get; }
        public byte[] Key { get { return _key; } }
        public byte[] Vector { get { return _vector; } }
    }
}
