namespace XmlParser.Interpret;

public class AttributeExpression : IExpression
{
    private readonly string _attributeName;
    private readonly string _attributeValue;

    public AttributeExpression(string attributeName, string attributeValue)
    {
        _attributeName = attributeName;
        _attributeValue = attributeValue;
    }

    public List<XmlElement> Interpret(XmlElement context)
    {
        var result = new List<XmlElement>();

        if (context.Attributes.Any(attr => attr.Name == _attributeName && attr.Value == _attributeValue))
            result.Add(context);

        foreach (var child in context.Children)
            result.AddRange(new AttributeExpression(_attributeName, _attributeValue).Interpret(child));
        
        return result;
    }
}