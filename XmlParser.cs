using XmlParser.Intefaces;

namespace XmlParser;

public class XmlParser : IXmlParser
{
    private readonly IXmlReader _xmlReader;
    private readonly IXmlElementParser _elementParser;

    public XmlParser(IXmlReader xmlReader, IXmlElementParser elementParser)
    {
        _xmlReader = xmlReader;
        _elementParser = elementParser;
    }
    
    public XmlDocument Parse(string path)
    {
        var xmlData = _xmlReader.Read(path);
        var rootElement = _elementParser.Parse(xmlData);
        
        return new XmlDocument(
            rootElement.headerAttributes,
            rootElement.root);
    }
}