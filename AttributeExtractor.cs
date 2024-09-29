using System.Text.RegularExpressions;
using XmlParser.Intefaces;

namespace XmlParser;

public class AttributeExtractor : IAttributeExtractor
{
    private const string AttributePattern = @"(\w+)\s*=\s*""([^""]*)""";

    public List<Attribute> ExtractAttributes(string attributesString)
    {
        MatchCollection matches = Regex.Matches(attributesString, AttributePattern);
        var result = new List<Attribute>();

        foreach (Match match in matches)
        {
            result.Add(new Attribute(
                match.Groups[1].Value,
                match.Groups[2].Value));
        }

        return result;
    }
}