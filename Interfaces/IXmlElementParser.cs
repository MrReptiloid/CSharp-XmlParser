using XmlParser.Models;

namespace XmlParser.Interfaces;

public interface IXmlElementParser
{
    ParseResult Parse(string xmlData);
}