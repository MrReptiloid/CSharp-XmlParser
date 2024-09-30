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
            Match headerMatch = regexStore.HeaderRegex.Match(xmlData);
            Match selfClosingTagMatch = regexStore.SelfClosingTagRegex.Match(xmlData);
            Match openTagMatch = regexStore.OpenTagRegex.Match(xmlData);
            Match closeTagMatch = regexStore.CloseTagRegex.Match(xmlData);
            Match textMatch = regexStore.TextRegex.Match(xmlData);
            
            Match? firstMatch = GetFirstMatch(
                headerMatch,
                selfClosingTagMatch,
                openTagMatch,
                closeTagMatch,
                textMatch);
            
            if (firstMatch is null)
                return (headerAttributes, root);
            
            xmlData = xmlData.RemoveFirstOccurrence(firstMatch.Value);

            if (firstMatch.Index == selfClosingTagMatch.Index)
            {
                XmlElement element = TagParse(selfClosingTagMatch);
                element.IsSelfClosing = true;
                
                if (root is null) root = element;
                else elements.Peek().Children.Add(element);
            }
            else if (firstMatch.Index == openTagMatch.Index)
            {
                XmlElement element = TagParse(openTagMatch);
                if (root is null) root = element;
                else elements.Peek().Children.Add(element);

                elements.Push(element);
            }
            else if (firstMatch.Index == headerMatch.Index)
            {
                headerAttributes = HeaderParse(headerMatch);
            }
            else if (firstMatch.Index == textMatch.Index)
            {
                TextParse(textMatch, ref elements);
            }
            else if (firstMatch.Index == closeTagMatch.Index)
            {
                elements.Pop();
            }
            else return (headerAttributes, root);
        }
    }
    
    private Match? GetFirstMatch(params Match[] matches) =>
        matches.Where(m => m.Success).MinBy(m => m.Index);

    private List<XmlAttribute> HeaderParse(Match headerContent) => 
        attributeExtractor.ExtractAttributes(headerContent.Groups[0].Value);
    
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

    private void TextParse(Match textMatch, ref Stack<XmlElement> elements)
    {
        string textValue = textMatch.Groups[1].Value.Trim();
        if (textValue.Length > 0 && elements.Count > 0)
            elements.Peek().Value = textValue;
    }
}