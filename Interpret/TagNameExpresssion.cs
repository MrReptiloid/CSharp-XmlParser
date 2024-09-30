using XmlParser.Models;

namespace XmlParser.Interpret;

public class TagNameExpression(string tagName) : IExpression
{
    public List<XmlElement> Interpret(XmlElement context)
    {
        List<XmlElement> result = new List<XmlElement>();

        if (context.TagName == tagName)
            result.Add(context);

        foreach (var child in context.Children)
            result.AddRange(new TagNameExpression(tagName).Interpret(child));

        return result;
    }
}