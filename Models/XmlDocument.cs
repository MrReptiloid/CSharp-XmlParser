namespace XmlParser.Models;

public record XmlDocument(List<XmlAttribute> HeaderAttributes, XmlElement RootElement);