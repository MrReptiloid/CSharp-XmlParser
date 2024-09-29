using System.Text.RegularExpressions;
using XmlParser.Intefaces;

namespace XmlParser;

public class XmlElementParser : IXmlElementParser
{
    private const string HeaderPattern = @"<\?xml\s(.*?)\?>";
    private const string OpenTagPattern = @"<(\w+)([^<>]*)>";
    private const string SelfClosingTagPattern = @"<(\w+)([^>]*)/>"; 
    private const string CloseTagPattern = @"</(.*?)>";
    private const string TextPattern = @">(\S[^<>]+)<";
    private readonly IAttributeExtractor _attributeExtractor;

    public XmlElementParser(IAttributeExtractor attributeExtractor)
    {
        _attributeExtractor = attributeExtractor;
    }

    public (List<Attribute> headerAttributes, XmlElement root) Parse(string xmlData)
    {
        Stack<XmlElement> elements = new Stack<XmlElement>();
        XmlElement? root = null;
        List<Attribute> headerAttributes = new List<Attribute>();
        
        while (true)
        {
            Match headerMatch = Regex.Match(xmlData, HeaderPattern);
            Match selfClosingTagMatch = Regex.Match(xmlData, SelfClosingTagPattern);
            Match openTagMatch = Regex.Match(xmlData, OpenTagPattern);
            Match closeTagMatch = Regex.Match(xmlData, CloseTagPattern);
            Match textMatch = Regex.Match(xmlData, TextPattern);
            var firstMatch = GetFirstMatch(
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
                var element = TagParse(selfClosingTagMatch);
                element.IsSelfClosing = true;
                if (root is null) root = element;
                else elements.Peek().Children.Add(element);
            }
            else if (firstMatch.Index == openTagMatch.Index)
            {
                var element = TagParse(openTagMatch);
                if (root is null) root = element;
                else elements.Peek().Children.Add(element);

                elements.Push(element);
            }
            else if (firstMatch.Index == headerMatch.Index)
                headerAttributes = HeaderParse(headerMatch);
            else if (firstMatch.Index == textMatch.Index)
                TextParse(textMatch, ref elements);
            else if (firstMatch.Index == closeTagMatch.Index)
                elements.Pop();
            else return (headerAttributes, root);
        }
    }
    
    private Match? GetFirstMatch(params Match[] matches) =>
        matches.Where(m => m.Success).MinBy(m => m.Index);

    private List<Attribute> HeaderParse(Match headerContent) => 
        _attributeExtractor.ExtractAttributes(headerContent.Groups[0].Value);
    
    private XmlElement TagParse(Match tagContent)
    {
        string tagName = tagContent.Groups[1].Value;

        var element = new XmlElement()
        {
            TagName = tagName,
            Attributes = _attributeExtractor.ExtractAttributes(tagContent.Groups[2].Value)
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