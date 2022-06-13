namespace Valet.Models;

public static class StringExtensions
{
    public static string EscapeIfNeeded(this string str)
    {
        return !str.Contains(' ') ? str : $"\"{str}\"";
    }
}