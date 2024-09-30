using XmlParser.Models;

namespace XmlParser.Interpret;

public interface IExpression
{
    List<XmlElement> Interpret(XmlElement context);
}