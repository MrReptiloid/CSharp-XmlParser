using System.Text.RegularExpressions;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public class AttributeExtractor : IAttributeExtractor
{
    private const string AttributePattern = @"(\w+)\s*=\s*""([^""]*)""";
    private readonly Regex _regex = new Regex(AttributePattern, RegexOptions.Compiled);

    public List<XmlAttribute> ExtractAttributes(string attributesString)
    {
        MatchCollection matches = _regex.Matches(attributesString);
        List<XmlAttribute> result = new List<XmlAttribute>(matches.Count);

        foreach (Match match in matches)
        {
            result.Add(new XmlAttribute(
                match.Groups[1].Value,
                match.Groups[2].Value));
        }

        return result;
    }
}