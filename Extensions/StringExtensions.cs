namespace XmlParser.Extensions;

public static class StringExtensions
{
    public static string RemoveFirstOccurrence(this string text, string textToRemove)
    {
        int startIndex = text.IndexOf(textToRemove, StringComparison.Ordinal);

        if (startIndex != -1)
        {
            return text.Remove(startIndex, textToRemove.Length);
        }
        
        return text;
    }
}