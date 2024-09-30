namespace XmlParser.Models;

public class XmlDocument(List<XmlAttribute> headerAttributes, XmlElement rootElement)
{
    public List<XmlAttribute> HeaderAttributes { get; } = headerAttributes;
    public XmlElement RootElement { get; } = rootElement;
}