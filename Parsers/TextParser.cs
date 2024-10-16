using System.Text.RegularExpressions;
using XmlParser.Abstractions;
using XmlParser.Enums;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser.Parsers;

public sealed class TextParser : Parser
{
    public TextParser() : base(ElementType.Text)
    {
        
    }

    public override Match? Parse(string xmlData, IRegexStore regexStore)
    {
        return regexStore.TextRegex.Match(xmlData);
    }
    
    public override void Execute(Match match, ParseResult result, Stack<XmlElement> elements)
    {
        string textValue = match.Groups[1].Value.Trim();

        if (textValue.Length > 0 && elements.Count > 0)
            elements.Peek().Value = textValue;
    }

}