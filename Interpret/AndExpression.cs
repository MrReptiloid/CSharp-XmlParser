using XmlParser.Models;

namespace XmlParser.Interpret;

public class AndExpression : IExpression
{
    private readonly IExpression _expression1;
    private readonly IExpression _expression2;

    public AndExpression(IExpression expression1, IExpression expression2)
    {
        _expression1 = expression1;
        _expression2 = expression2;
    }

    public List<XmlElement> Interpret(XmlElement context)
    {
        List<XmlElement> result1 = _expression1.Interpret(context);
        List<XmlElement> result2 = _expression2.Interpret(context);

        return result1.Intersect(result2).ToList();
    }
}