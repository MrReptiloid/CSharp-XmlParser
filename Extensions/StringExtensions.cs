using System.Diagnostics.CodeAnalysis;

namespace XmlParser;

public static class StringExtensions
{
    public static string RemoveFirstOccurrence(this string text, string textToRemove)
    {
        if (text == null)
            throw new ArgumentNullException(nameof(text), "Input Text Cannot Be null");

        if (textToRemove == null)
            throw new ArgumentNullException(nameof(textToRemove), "Text to Remove Cannot Be null");

        int startIndex = text.IndexOf(textToRemove, StringComparison.Ordinal);

        if (startIndex == -1)
            return text;
        
        return text.Remove(startIndex, textToRemove.Length);
    }
}