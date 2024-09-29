namespace XmlParser.Intefaces;

public interface IXmlElementParser
{
    (List<Attribute> headerAttributes, XmlElement root) Parse(string xmlData);
}