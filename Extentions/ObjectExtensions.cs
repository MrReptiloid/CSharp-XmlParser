using YamlDotNet.Serialization;

namespace XmlParser;

public static class ObjectExtensions
{
    public static void WriteYaml(this object obj)
    {
        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(obj);
        Console.WriteLine(yaml);
    }
}