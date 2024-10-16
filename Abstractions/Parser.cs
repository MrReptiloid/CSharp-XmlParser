using System.Text.RegularExpressions;
using XmlParser.Enums;
using XmlParser.Interfaces;
using XmlParser.Models;

namespace XmlParser.Abstractions;

public abstract class Parser : IEquatable<Parser>, IAction
{
    protected readonly ElementType _type;

    protected Parser(ElementType type)
    {
        _type = type;
    }

    public override int GetHashCode() => (int)_type;
    public abstract Match? Parse(string xmlData, IRegexStore regexStore);

    public abstract void Execute(Match match, ParseResult result, Stack<XmlElement> elements);
    
    public bool Equals(Parser? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _type == other._type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Parser)obj);
    }
}