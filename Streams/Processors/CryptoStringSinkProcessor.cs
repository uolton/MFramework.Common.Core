using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.Streams.Processors
{
    public class CryptoStringSinkProcessor : StreamSinkProcessor<MemoryStream>
    {
        public CryptoStringSinkProcessor(string cryptoString)
            : base(new MemoryStream(cryptoString.ToCypherBuffer()))
        {
            
        }
    }
}
