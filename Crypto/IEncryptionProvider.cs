namespace MFramework.Common.Core.Crypto
{
    public interface IEncryptionProvider<TKey> where TKey:IEncryptionKey
    {
        IEncryptor GetEncryptor(TKey key);
        IDecryptor GetDecryptor(TKey key);
    }
}
