using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams.Processors
{
    public class CryptoStreamSinkProcessor:StreamSinkProcessor<CryptoStream>
    {
        public CryptoStreamSinkProcessor(CryptoStream stream) : base(stream)
        {
        }

        protected override void DoFlush()
        {
           
            if (! _stream.HasFlushedFinalBlock) _stream.FlushFinalBlock();
            base.DoFlush();
        }
    }
}
