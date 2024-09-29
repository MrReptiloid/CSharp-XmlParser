namespace XmlParser.Interpret;

public class TagNameExpression : IExpression 
{
    private readonly string _tagName;

    public TagNameExpression(string tagName)
    {
        _tagName = tagName;
    }

    public List<XmlElement> Interpret(XmlElement context)
    {
        var result = new List<XmlElement>();

        if (context.TagName == _tagName)
            result.Add(context);

        foreach (var child in context.Children)
            result.AddRange(new TagNameExpression(_tagName).Interpret(child));

        return result;
    }
}