using System.Text;

namespace XmlParser;

public interface IXmlParser
{
    public XmlDocument Parse(string path);
}