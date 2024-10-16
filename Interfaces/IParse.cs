using System.Text.RegularExpressions;

namespace XmlParser.Interfaces;

public interface IParse
{
    Match? Parse(string data);
}