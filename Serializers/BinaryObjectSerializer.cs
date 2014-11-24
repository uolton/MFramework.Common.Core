using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MFramework.Common.Core.Serializers
{
    class BinaryObjectSerializer:IObjectSerializer
    {

        private BinaryFormatter _formatter;
        public BinaryObjectSerializer() : this(StreamingContextStates.Remoting) { }
        public BinaryObjectSerializer(StreamingContextStates contextState)
        {
            _formatter = new BinaryFormatter(null, new StreamingContext(contextState));
        }
        public void Serialize(Stream stream, object @object)
        {
            throw new NotImplementedException();
        }
    }
}
