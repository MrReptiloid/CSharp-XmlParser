using System.Text.RegularExpressions;
using XmlParser.Abstractions;
using XmlParser.Enums;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser.Parsers;

public sealed class TagParser : Parser
{
    private readonly IAttributeExtractor _attributeExtractor;

    public TagParser(ElementType type, IAttributeExtractor attributeExtractor)
        : base(type)
    {
        _attributeExtractor = attributeExtractor;
    }

    public override Match? Parse(string xmlData, IRegexStore regexStore) => _type == ElementType.SelfClosingTag
        ? regexStore.SelfClosingTagRegex.Match(xmlData)
        : regexStore.OpenTagRegex.Match(xmlData);

    public override void Execute(Match match, ParseResult result, Stack<XmlElement> elements)
    {
        XmlElement element = TagParse(match);

        if (_type == ElementType.SelfClosingTag)
        {
            element.IsSelfClosing = true;
        }

        if (result.Root is null)
        {
            result.Root = element;
        }
        else
        {
            elements.Peek().Childrens.Add(element);
        }

        if (_type == ElementType.OpenTag)
        {
            elements.Push(element);
        }
    }
    
    private XmlElement TagParse(Match tagContent)
    {
        string tagName = tagContent.Groups[1].Value;

        return new XmlElement
        {
            TagName = tagName,
            Attributes = _attributeExtractor.ExtractAttributes(tagContent.Groups[2].Value)
        };
    }
    
}
