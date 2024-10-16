using XmlParser.Models;

namespace XmlParser.Interpret;

public class AttributeExpression(string attributeName, string attributeValue) : IExpression
{
    public List<XmlElement> Interpret(XmlElement context)
    {
        List<XmlElement> result = [];

        if (context.Attributes.Any(attr => attr.Name == attributeName && attr.Value == attributeValue))
        {
            result.Add(context);
        }

        foreach (var child in context.Childrens)
        {
            result.AddRange(new AttributeExpression(attributeName, attributeValue).Interpret(child));
        }
        return result;
    }
}