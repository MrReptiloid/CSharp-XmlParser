using XmlParser.Interpret;

namespace XmlParser;

public class Program
{
    public static void Main(string[] args)
    {
        var xmlReader = new FileXmlReader();
        var attributeExtractor = new AttributeExtractor();
        var elementParser = new XmlElementParser(attributeExtractor);

        var xmlParser = new XmlParser(xmlReader, elementParser);
        XmlDocument xmlDocument = xmlParser.Parse("../../../Data/test.xml");

        xmlDocument.WriteYaml();
    }
}
