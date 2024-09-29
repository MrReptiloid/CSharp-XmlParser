using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using XmlParser.Intefaces;

namespace XmlParser;

public class XmlElementParser : IXmlElementParser
{
    private const string HeaderPattern = @"<\?xml\s(.*?)\?>";
    private const string OpenTagPattern = @"<(\w+)([^<>]*)>";
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
            Match openTagMatch = Regex.Match(xmlData, OpenTagPattern);
            Match closeTagMatch = Regex.Match(xmlData, CloseTagPattern);
            Match textMatch = Regex.Match(xmlData, TextPattern);
            var minIndex = GetMinIndex(headerMatch, openTagMatch, closeTagMatch, textMatch);

            if (minIndex == headerMatch.Index)
            {
                headerAttributes = HeaderParse(headerMatch);
                xmlData = xmlData.RemoveFirstOccurrence(headerMatch.Value);
            }
            else if (minIndex == closeTagMatch.Index)
            {
                elements.Pop();
                xmlData = xmlData.RemoveFirstOccurrence(closeTagMatch.Value);
            }
            else if (minIndex == openTagMatch.Index)
            {
                OpenTagParse(openTagMatch, ref elements, ref root);
                xmlData = xmlData.RemoveFirstOccurrence(openTagMatch.Groups[1].Value);
            }
            else if (minIndex == textMatch.Index)
            {
                TextParse(textMatch, ref elements);
                xmlData = xmlData.RemoveFirstOccurrence(textMatch.Groups[1].Value);
            }
            else return (headerAttributes, root);
        }
    }
    
    public int GetMinIndex(params Match[] matches)
    {
        List<int> positionsList = matches.Where(m => m.Success).Select(m => m.Index).ToList();
        
        if (positionsList.Count == 0)
            return -1;
        
        return positionsList.Min();
    }

    public List<Attribute> HeaderParse(Match headerContent)
    {
        return _attributeExtractor.ExtractAttributes(headerContent.Groups[0].Value);
    }
    
    public void OpenTagParse(Match tagContent, ref Stack<XmlElement> elements, ref XmlElement root)
    {
        string tagName = tagContent.Groups[1].Value;

        var element = new XmlElement()
        {   
            TagName = tagName,
            Attributes = _attributeExtractor.ExtractAttributes(tagContent.Groups[2].Value)
        };
        
        if (root is null) root = element;
        else elements.Peek().Children.Add(element);

        elements.Push(element);
    }

    public void TextParse(Match textMatch, ref Stack<XmlElement> elements)
    {
        string textValue = textMatch.Groups[1].Value.Trim();
        if (textValue.Length > 0 && elements.Count > 0)
            elements.Peek().Value = textValue;
    }
}