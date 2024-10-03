using XmlParser.Extensions;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public class Program
{
    public static void Main(string[] args)
    {
        FileXmlReader xmlReader = new FileXmlReader();
        IRegexStore regexStore = new RegexStore();
        IAttributeExtractor attributeExtractor = new AttributeExtractor();
        
        XmlElementParser elementParser = new XmlElementParser(regexStore, attributeExtractor);

        XmlParser xmlParser = new XmlParser(xmlReader, elementParser);
        XmlDocument? xmlDocument = xmlParser.Parse("../../../Data/test.xml");

        xmlDocument?.WriteYaml();
    }
}
