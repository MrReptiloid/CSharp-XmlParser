using System.Text.RegularExpressions;

namespace XmlParser.Interfaces;

public interface IRegexStore
{
    public Regex HeaderRegex { get; } 
    public Regex OpenTagRegex { get; }
    public Regex SelfClosingTagRegex { get; }
    public Regex CloseTagRegex { get; }
    public Regex TextRegex { get; }
}