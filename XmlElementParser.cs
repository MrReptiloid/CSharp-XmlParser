using System.Text.RegularExpressions;
using XmlParser.Abstractions;
using XmlParser.Enums;
using XmlParser.Extensions;
using XmlParser.Interfaces;
using XmlParser.Models;
using XmlParser.Parsers;

namespace XmlParser;

public sealed class XmlElementParser
    : IXmlElementParser
{
    private readonly IRegexStore _regexStore;
    
    private readonly Dictionary<ElementType, Parser> _parsers;

    public XmlElementParser(IAttributeExtractor attributeExtractor, IRegexStore regexStore)
    {
        _regexStore = regexStore;
        _parsers = new()
        {
            { ElementType.Header, new HeaderParser(attributeExtractor) },
            { ElementType.OpenTag, new TagParser(ElementType.OpenTag, attributeExtractor) },
            { ElementType.SelfClosingTag, new TagParser(ElementType.SelfClosingTag, attributeExtractor) },
            { ElementType.Text, new TextParser() },
            { ElementType.CloseTag, new CloseTagParser() }
        };
    }

    public ParseResult Parse(string xmlData)
    {
        Stack<XmlElement> elements = new Stack<XmlElement>();
        ParseResult? result = new ParseResult(); 
        
        while (true)
        {
            Match firstMatch = GetFirstMatch(xmlData);
            if(firstMatch == null)
                break;

            var parser = _parsers.FirstOrDefault(p => p.Value.Parse(xmlData, _regexStore).Value == firstMatch.Value).Value;
            parser?.Execute(firstMatch, result, elements);

            xmlData = xmlData.RemoveFirstOccurrence(firstMatch.Value);
        }

        return result;
    }

    private Match? GetFirstMatch(string xmlData)
    {
        var matches = _parsers.Select(p => p.Value.Parse(xmlData, _regexStore)).Where(m => m.Success);
        return matches.MinBy(m => m.Index);
    }
}