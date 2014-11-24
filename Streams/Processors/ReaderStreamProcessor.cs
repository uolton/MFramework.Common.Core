using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams.Processors
{
    public class ReaderStreamProcessor: StreamProcessorBase, IStreamFilterRoleReader
    {
        private Stream _stream;
        public override void Bind(IStreamFilterRoleWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Bind(IStreamFilterRoleReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void DoClose()
        {
            _stream.Close();
        }

        public void ReadFrom(Stream stream)
        {
            _stream = stream;
        }

        public string Read()
        {
            var buffer = new MemoryStream();
            _stream.CopyTo(buffer);
            return Encoding.UTF8.GetString(buffer.ToArray()).TrimEnd('\0');

        }
    }
}
