using XmlParser.Intefaces;

namespace XmlParser;

public class FileXmlReader : IXmlReader
{
    public string Read(string path) => File.ReadAllText(path);
}