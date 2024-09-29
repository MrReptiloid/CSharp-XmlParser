using System.CodeDom;
using System.Xml.Schema;

namespace XmlParser;

public class XmlElement
{
    public string? TagName { get; set; }
    public List<Attribute> Attributes { get; set; }
    public List<XmlElement> Children { get; set; } = new List<XmlElement>();
    public string? Value { get; set; }
    public bool IsSelfClosing { get; set; }

    public static void Print(XmlElement element, int indent)
    {
        Console.WriteLine(new string(' ', indent * 2) + element.TagName);
        
        foreach (var attribute in element.Attributes)
            Console.WriteLine(new string(' ', (indent + 1) * 2) + $"{attribute.Name}='{attribute.Value}'");
        
        if(!string.IsNullOrEmpty(element.Value))
            Console.WriteLine(new string(' ', (indent + 1) *2 ) + element.Value);

        foreach (var child in element.Children)
            Print(child, indent + 1);
    }
}  