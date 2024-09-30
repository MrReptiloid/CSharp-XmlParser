using XmlParser.Models;

namespace XmlParser.Interfaces;

public interface IAttributeExtractor
{
    List<XmlAttribute> ExtractAttributes(string attributes);
}