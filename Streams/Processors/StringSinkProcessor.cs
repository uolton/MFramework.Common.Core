using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.Streams.Processors
{
    public class StringSinkProcessor:StreamSinkProcessor<MemoryStream>
    {

        private string _text;
        public StringSinkProcessor() : base(new MemoryStream())
        {
            _text = string.Empty;
        }   
        public StringSinkProcessor(string text)
            : base(new MemoryStream(Encoding.UTF8.GetBytes(text)))
        {
            _stream.Rewind();
        }
        public string Value
        {
            get
            {
                Close();
                return _text ;
            }
        }

        

        protected override void DoFlush()
        {
            _text=_stream.ToArray().CypherBufferToString();
            
        }
    }
}
