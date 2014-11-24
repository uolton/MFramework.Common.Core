using System.IO;

namespace MFramework.Common.Core.Serializers
{
    interface IObjectSerializer
    {
        void Serialize(Stream stream, object @object);
    }
}
