using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public sealed class XmlParser(IXmlReader xmlReader, IXmlElementParser elementParser) : IXmlParser
{
    public XmlDocument? Parse(string path)
    {
        try
        {
            string xmlData = xmlReader.Read(path);
            ParseResult result = elementParser.Parse(xmlData);

            return new XmlDocument(
                result.HeaderAttributes,
                result.Root);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}