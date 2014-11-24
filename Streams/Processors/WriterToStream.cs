using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams.Processors
{
    public class WriterToStream : StreamProcessorBase, IStreamFilterRoleWriter
    {
        private StreamWriter _streamWriter;
        private Stream _stream;
        public WriterToStream (){}
        public override void Bind(IStreamFilterRoleWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Bind(IStreamFilterRoleReader reader)
        {
            throw new NotImplementedException();
        }

        public WriterToStream Write(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(text);
            _stream.Write(buffer,0,buffer.Length);
            //_streamWriter.Write(Encoding.UTF8.GetBytes(text));
            return (this);
        }

        protected override void DoFlush()
        {
            //_streamWriter.Flush();
            _stream.Flush();
            base.DoFlush();
        }

        protected override void DoClose()
        {
           //_streamWriter.Close();
            _stream.Close();
        }

        public void WriteTo(Stream stream)
        {
            _streamWriter = new StreamWriter(stream);
            _stream = stream;
        }
    }
}
