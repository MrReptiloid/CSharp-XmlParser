using System.Text.RegularExpressions;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser;

public class XmlElementParser(IRegexStore regexStore, IAttributeExtractor attributeExtractor)
    : IXmlElementParser
{
    public (List<XmlAttribute> headerAttributes, XmlElement root) Parse(string xmlData)
    {
        Stack<XmlElement> elements = new Stack<XmlElement>();
        XmlElement? root = null;
        List<XmlAttribute> headerAttributes = new List<XmlAttribute>();
        
        while (true)
        {
            Match? firstMatch = GetFirstMatch(
                regexStore.HeaderRegex.Match(xmlData),
                regexStore.SelfClosingTagRegex.Match(xmlData),
                regexStore.OpenTagRegex.Match(xmlData),
                regexStore.CloseTagRegex.Match(xmlData),
                regexStore.TextRegex.Match(xmlData));
            
            if (firstMatch is null)
                return (headerAttributes, root);
            
            switch (firstMatch)
            {
                case var a when firstMatch.Value == regexStore.HeaderRegex.Match(xmlData).Value:
                    headerAttributes = ParseHeader(a);
                    break;
                case var _ when firstMatch.Value == regexStore.SelfClosingTagRegex.Match(xmlData).Value:
                    HandleSelfClosingTag(firstMatch, ref root, elements); 
                    break;
                case var _ when firstMatch.Value == regexStore.OpenTagRegex.Match(xmlData).Value:
                    HandleOpenTag(firstMatch, ref root, elements);
                    break;
                case var _ when firstMatch.Value == regexStore.TextRegex.Match(xmlData).Value:
                    HandleText(firstMatch, elements);
                    break;
                case var _ when firstMatch.Value == regexStore.CloseTagRegex.Match(xmlData).Value:
                    HandleCloseTag(elements);
                    break;
                default:
                    return (headerAttributes, root);
            }
            
            xmlData = xmlData.RemoveFirstOccurrence(firstMatch.Value);
        }
    }

    private void HandleSelfClosingTag(Match match, ref XmlElement? root, Stack<XmlElement> elements)
    {
        XmlElement element = TagParse(match);
        element.IsSelfClosing = true;

        if (root is null)
            root = element;
        else
            elements.Peek().Children.Add(element);
    }
    
    private List<XmlAttribute> ParseHeader(Match headerContent) => 
        attributeExtractor.ExtractAttributes(headerContent.Groups[0].Value);

    private void HandleOpenTag(Match match, ref XmlElement? root, Stack<XmlElement> elements)
    {
        XmlElement element = TagParse(match);

        if (root is null)
            root = element;
        else
            elements.Peek().Children.Add(element);
        
        elements.Push(element);
    }

    private void HandleText(Match match, Stack<XmlElement> elements)
    {
        string textValue = match.Groups[1].Value.Trim();

        if (textValue.Length > 0 && elements.Count > 0)
            elements.Peek().Value = textValue;
    }

    private void HandleCloseTag(Stack<XmlElement> elements)
    {
        if (elements.Count > 0)
            elements.Pop();
    }
    
    private Match? GetFirstMatch(params Match[] matches) =>
        matches.Where(m => m.Success).MinBy(m => m.Index);

    
    private XmlElement TagParse(Match tagContent)
    {
        string tagName = tagContent.Groups[1].Value;

        XmlElement element = new XmlElement()
        {
            TagName = tagName,
            Attributes = attributeExtractor.ExtractAttributes(tagContent.Groups[2].Value)
        };

        return element;
    }
}