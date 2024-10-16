using System.Text.RegularExpressions;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public sealed class AttributeExtractor : IAttributeExtractor
{
    private const string AttributePattern = @"(?<name>\w+)\s*=\s*""(?<value>[^""]*)""";
    private readonly Regex _regex = new(AttributePattern, RegexOptions.Compiled, TimeSpan.FromSeconds(30));
    
    public List<XmlAttribute> ExtractAttributes(string attributesString)
    {
        try
        {
            MatchCollection matches = _regex.Matches(attributesString);
            return Foo(matches);
        }
        catch (Exception)
        {
            return [];
        }
    }

    private static List<XmlAttribute> Foo(MatchCollection matches)
    {
        const string nameKey = "name";
        const string valueKey = "value";
        List<XmlAttribute> result = [];
        
        foreach (Match match in matches)
        {
            Group nameGroup = match.Groups[nameKey];
            Group valueGroup = match.Groups[valueKey];

            if (nameGroup.Success && valueGroup.Success)
            {
                result.Add(new XmlAttribute(nameGroup.Value, valueGroup.Value));
            }
        }

        return result;
    }
}