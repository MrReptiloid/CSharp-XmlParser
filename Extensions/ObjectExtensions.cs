using YamlDotNet.Serialization;

namespace XmlParser.Extensions;

public static class ObjectExtensions
{
    public static void WriteYaml(this object obj)
    {
        ISerializer serializer = new SerializerBuilder().Build();
        string yaml = serializer.Serialize(obj);
        Console.WriteLine(yaml);
    }
}