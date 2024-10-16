using System.Text.RegularExpressions;
using XmlParser.Abstractions;
using XmlParser.Enums;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser.Parsers;

public sealed class CloseTagParser : Parser
{
    public CloseTagParser() : base(ElementType.CloseTag)
    {
        
    }

    public override Match? Parse(string xmlData, IRegexStore regexStore)
    {
        return regexStore.CloseTagRegex.Match(xmlData);
    }
    
    public override void Execute(Match match, ParseResult result, Stack<XmlElement> elements)
    {
        if (elements.Count > 0)
        {
            elements.Pop();
        }
    }
}