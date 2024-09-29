namespace XmlParser.Interpret;

public class TextExpression : IExpression
{
    private readonly string _textValue;

    public TextExpression(string textValue)
    {
        _textValue = textValue;
    }

    public List<XmlElement> Interpret(XmlElement context)
    {
        var result = new List<XmlElement>();
        
        if (context.Value != null && context.Value.Contains(_textValue))
            result.Add(context);

        foreach (var child in context.Children)
            result.AddRange(new TextExpression(_textValue).Interpret(child));

        return result;
    }
}