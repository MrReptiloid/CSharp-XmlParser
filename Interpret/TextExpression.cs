using XmlParser.Models;

namespace XmlParser.Interpret;

public class TextExpression(string textValue) : IExpression
{
    public List<XmlElement> Interpret(XmlElement context)
    {
        List<XmlElement> result = new List<XmlElement>();
        
        if (context.Value != null && context.Value.Contains(textValue))
            result.Add(context);

        foreach (var child in context.Children)
            result.AddRange(new TextExpression(textValue).Interpret(child));

        return result;
    }
}