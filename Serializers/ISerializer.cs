namespace MFramework.Common.Core.Serializers
{
    public interface ISerializer

    {
        string Serialize<T>(T @object);
        T Deserialize<T>(string serializedObject);
    }
}
