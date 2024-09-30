using XmlParser.Models;

namespace XmlParser.Interfaces;

public interface IXmlParser
{
    public XmlDocument Parse(string path);
}