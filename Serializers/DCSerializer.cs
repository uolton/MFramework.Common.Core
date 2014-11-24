using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.Serializers
{
    public class DCSerializer:ISerializer
    {
        public  string Serialize<T>(T @object)
        {
            var ser = new DataContractSerializer(typeof (T));
            
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                ser.WriteObject(writer, @object);
            }
            return sb.ToString();
        }

        public T Deserialize<T>(string serializedObject)
        {
            var deserializer = new  DataContractSerializer(typeof (T));
            object obj = deserializer.ReadObject(serializedObject.ToStream<UnicodeEncoding>());
            return (T)obj;

        }
    }
}
