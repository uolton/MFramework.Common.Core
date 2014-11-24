using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Crypto;

namespace MFramework.Common.Core.Streams.Processors
{
    public class StreamDecypher<TEncriptionProvider, TKey> : StreamProcessorBase, IStreamFilterRoleReader
        where TEncriptionProvider : class,IEncryptionProvider<TKey>, new()
        where TKey : IEncryptionKey
    {
        private IDecryptor _decryptor;
        private Stream _source;
        private CryptoStream _cryptoStream;
        public StreamDecypher(TKey useKey)
        {

            _decryptor = new TEncriptionProvider().GetDecryptor(useKey);

        }

        public override void Bind(IStreamFilterRoleWriter writer)
        {
            
        }

        public override void Bind(IStreamFilterRoleReader reader)
        {
            _cryptoStream = _decryptor.Decrypt(_source);
            
            reader.ReadFrom(_cryptoStream);
            _prev = (IStreamProcessor) reader;
        }

        protected override void DoFlush()
        {
            _cryptoStream.FlushFinalBlock();
        }

        protected override void DoClose()
        {
            _cryptoStream.Close();
        }


        protected override void DoBind(IStreamProcessor p)
        {
            BindAsReaderFrom((IStreamPipe) p);
        }

        public void ReadFrom(Stream stream)
        {
            _source = stream;


        }
    }
}
