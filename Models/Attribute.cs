namespace XmlParser;

public class Attribute
{
    public string Name { get; } = string.Empty;
    public string Value { get; } = string.Empty;


    public Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
}