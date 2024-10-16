using System.Text.RegularExpressions;

namespace XmlParser.Interfaces;

public interface IAction
{
    Match? Parse(string xmlData, IRegexStore regexStore);
}