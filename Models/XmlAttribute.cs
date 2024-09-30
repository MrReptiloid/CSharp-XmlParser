namespace XmlParser.Models;

public class XmlAttribute(string name, string value)
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}