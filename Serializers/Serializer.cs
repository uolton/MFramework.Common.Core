namespace MFramework.Common.Core.Serializers
{
    public static class Serializer
    {
        public static  ISerializer XmlSerializer = new XmlSerializer();
        public static ISerializer DataContractSerializer = new DCSerializer();

        public static string Serialize<TSerializer, T>(this T @this) where TSerializer : class, ISerializer, new()
        {
            var ser = new TSerializer();
            return ser.Serialize(@this);
        }
        public static T Deserialize<TSerializer,T>(this string  @this) where TSerializer : class, ISerializer, new()
        {
            var ser = new TSerializer();
            return ser.Deserialize<T>(@this);
        }
    }
}
