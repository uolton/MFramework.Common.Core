using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Serializers;

namespace MFramework.Common.Core.Streams.Processors
{
    class StreamObjectSerializer<TSerializer>:StreamProcessorBase,IStreamFilterRoleWriter where TSerializer:class,IObjectSerializer,new()
    {
        
        private IObjectSerializer _serializer;
        private Stream _destination;
        public StreamObjectSerializer()
        {

            _serializer = new TSerializer();

        }

        public void Serialize<T>(T @object)
        {
            _serializer.Serialize(_destination, @object);
        }
        public override void Bind(IStreamFilterRoleWriter writer)
        {
            
        }

        public override void Bind(IStreamFilterRoleReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void DoClose()
        {
            
        }


        public void WriteTo(Stream stream)
        {
            _destination = stream;
        }
    }
}
