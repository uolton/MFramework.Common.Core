using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Streams;

namespace MFramework.Common.Core.Crypto
{
    public static class CryptoStreamExtension
    {
        public static TStream Encrypt<TEncryptionProvider, TKey, TStream>(this TStream @this, TKey key,
            TStream outStream)
            where TEncryptionProvider : class, IEncryptionProvider<TKey>, new()
            where TKey : IEncryptionKey
            where TStream : Stream

            {
                var chyper = new TEncryptionProvider().GetEncryptor(key);
                var chyperStream = chyper.Encrypt(outStream);
                
                @this.CopyTo(chyperStream);
                @this.Flush();
                chyperStream.FlushFinalBlock();
                //chyperStream.DoClose();    
                return outStream;

            }
        public static TStream Decrypt<TEncryptionProvider, TKey, TStream>(this TStream @this, TKey key, TStream outStream)
            where TEncryptionProvider : class, IEncryptionProvider<TKey>, new()
            where TKey : IEncryptionKey
            where TStream : Stream
        {
            var chyper = new TEncryptionProvider().GetDecryptor(key);
            var chyperStream = chyper.Decrypt(@this.Rewind());
            chyperStream.CopyTo(outStream);
            return outStream;

        }
    }
}
