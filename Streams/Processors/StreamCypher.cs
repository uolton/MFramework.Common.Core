using System.IO;
using System.Security.Cryptography;
using MFramework.Common.Core.Crypto;

namespace MFramework.Common.Core.Streams.Processors
{

    public class StreamCypher<TEncriptionProvider, TKey> : StreamProcessorBase ,IStreamFilterRoleWriter
        where TEncriptionProvider :class,IEncryptionProvider<TKey>,new ()
        where TKey:IEncryptionKey

    {
        private IEncryptor _encryptor;
        private Stream _destination;
        private CryptoStream _cryptoStream;
        public StreamCypher(TKey useKey)
        {
            
            _encryptor = new TEncriptionProvider().GetEncryptor(useKey);

        }

    public override void Bind(IStreamFilterRoleWriter writer)
    {
        _cryptoStream = _encryptor.Encrypt(_destination);
        writer.WriteTo(_cryptoStream);
        _prev = (IStreamProcessor)writer;
    }

        public override void Bind(IStreamFilterRoleReader reader)
        {
            throw new System.NotImplementedException();
        }

        protected override void DoFlush()
        {
             while (!_cryptoStream.HasFlushedFinalBlock)
                {
                        _cryptoStream.FlushFinalBlock();            
                }
            

        }

        protected override void DoClose()
        {
            _cryptoStream.Close();
        }

        public void WriteTo(Stream stream)
        {
            _destination = stream;
        }
    }
}
