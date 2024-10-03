using System.Text.RegularExpressions;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public class AttributeExtractor : IAttributeExtractor
{
    private const string AttributePattern = @"(?<name>\w+)\s*=\s*""(?<value>[^""]*)""";
    private readonly Regex _regex = new Regex(AttributePattern, RegexOptions.Compiled);
    
    public List<XmlAttribute> ExtractAttributes(string attributesString)
    {
        if (string.IsNullOrWhiteSpace(attributesString))
            return new List<XmlAttribute>();
        
        MatchCollection matches;
        try
        {
            matches = _regex.Matches(attributesString);
        }
        catch (RegexMatchTimeoutException)
        {
            return new List<XmlAttribute>();
        }
        
        List<XmlAttribute> result = new List<XmlAttribute>(matches.Count);

        foreach (Match match in matches)
        {
            Group nameGroup = match.Groups["name"];
            Group valueGroup = match.Groups["value"];

            if (nameGroup.Success && valueGroup.Success)
                result.Add(new XmlAttribute(nameGroup.Value, valueGroup.Value));
        }

        return result;
    }
}