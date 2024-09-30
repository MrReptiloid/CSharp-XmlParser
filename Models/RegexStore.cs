using System.Text.RegularExpressions;
using XmlParser.Interfaces;

namespace XmlParser.Models;

public class RegexStore : IRegexStore
{
    private const string HeaderPattern = @"<\?xml\s(.*?)\?>";
    private const string OpenTagPattern = @"(?<=<)(\w+)([^<>]*)(?=>)";
    private const string SelfClosingTagPattern = @"<(\w+)([^>]*)/>"; 
    private const string CloseTagPattern = @"</(.*?)>";
    private const string TextPattern = @">(\S[^<>]+)<";

    public Regex HeaderRegex { get; } = new Regex(HeaderPattern, RegexOptions.Compiled);
    public Regex OpenTagRegex { get; } = new Regex(OpenTagPattern, RegexOptions.Compiled);
    public Regex SelfClosingTagRegex { get; } = new Regex(SelfClosingTagPattern, RegexOptions.Compiled);
    public Regex CloseTagRegex { get; } = new Regex(CloseTagPattern, RegexOptions.Compiled);
    public Regex TextRegex { get; } = new Regex(TextPattern, RegexOptions.Compiled);
}