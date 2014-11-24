using System.IO;

namespace MFramework.Common.Core.Streams.Processors
{
    public class StreamSinkProcessor<TStream>:StreamProcessorBase where TStream:Stream
    {
        protected TStream _stream;
        
        public StreamSinkProcessor(TStream stream)
        {
            _stream = stream;
        }
        public override void Bind(IStreamFilterRoleWriter writer)
        {
            writer.WriteTo(_stream);
            _prev = (IStreamProcessor) writer;
        }

        public override void Bind(IStreamFilterRoleReader reader)
        {
            reader.ReadFrom(_stream);
            _prev = (IStreamProcessor)reader;
        }

        protected override void DoClose()
        {
            _stream.Close();
        }
    }
}
