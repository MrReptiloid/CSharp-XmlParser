using System.Text.RegularExpressions;
using XmlParser.Abstractions;
using XmlParser.Enums;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser.Parsers;

public sealed class HeaderParser : Parser
{
    private readonly IAttributeExtractor _attributeExtractor;
    public HeaderParser(IAttributeExtractor attributeExtractor) : base(ElementType.Header)
    {
        _attributeExtractor = attributeExtractor;
    }

    public override Match? Parse(string xmlData, IRegexStore regexStore)
    {
        return regexStore.HeaderRegex.Match(xmlData);
    }
    
    public override void Execute(Match match, ParseResult result, Stack<XmlElement> elements)
    {
        result.HeaderAttributes = _attributeExtractor.ExtractAttributes(match.Groups[0].Value);
    }
}