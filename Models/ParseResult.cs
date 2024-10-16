namespace XmlParser.Models;


public class ParseResult()
{
    public List<XmlAttribute>? HeaderAttributes { get; set; }
    public XmlElement? Root { get; set; }
} 