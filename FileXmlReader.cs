using XmlParser.Interfaces;

namespace XmlParser;

public class FileXmlReader : IXmlReader
{
    public string Read(string path) => File.ReadAllText(path);
}