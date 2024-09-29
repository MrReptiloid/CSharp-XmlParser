namespace XmlParser;

public static class StringExtensions
{
    public static string RemoveFirstOccurrence(this string text, string textToRemove) => 
        text.Remove(text.IndexOf(textToRemove), textToRemove.Length);
}