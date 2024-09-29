namespace XmlParser;

public class XmlDocument
{
    public XmlDocument(List<Attribute> headerAttributes, XmlElement rootElement)
    {
        HeaderAttributes = headerAttributes;
        RootElement = rootElement;
    }

    public List<Attribute> HeaderAttributes { get; }
    public XmlElement RootElement { get; }
}