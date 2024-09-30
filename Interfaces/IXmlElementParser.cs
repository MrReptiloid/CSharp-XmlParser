using XmlParser.Models;

namespace XmlParser.Interfaces;

public interface IXmlElementParser
{
    (List<XmlAttribute> headerAttributes, XmlElement root) Parse(string xmlData);
}