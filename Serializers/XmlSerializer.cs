using System.IO;
using System.Text;
using System.Xml;

namespace MFramework.Common.Core.Serializers
{
    public  class XmlSerializer:ISerializer
    {
        public  string Serialize<T>(T @object)
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                ser.Serialize(writer, @object);
            }
            return sb.ToString();
        }

        public T Deserialize<T>(string serializedObject)
        {
            System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            StringReader reader= new StringReader(serializedObject);
            object obj = deserializer.Deserialize(reader);
            return (T )obj;
            
        }
    }
}
