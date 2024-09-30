using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public class XmlParser(IXmlReader xmlReader, IXmlElementParser elementParser) : IXmlParser
{
    public XmlDocument Parse(string path)
    {
        string xmlData = xmlReader.Read(path);
        (List<XmlAttribute> headerAttributes, XmlElement root) rootElement =
            elementParser.Parse(xmlData);
        
        return new XmlDocument(
            rootElement.headerAttributes,
            rootElement.root);
    }
}