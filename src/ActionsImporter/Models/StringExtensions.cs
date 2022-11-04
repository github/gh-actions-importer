namespace ActionsImporter.Models;

public static class StringExtensions
{
    public static string EscapeIfNeeded(this string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        return !str.Contains(' ', StringComparison.Ordinal) ? str : $"\"{str}\"";
    }
}
