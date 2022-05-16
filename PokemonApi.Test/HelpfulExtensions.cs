namespace PokemonApi.Test;

internal static class HelpfulExtensions
{
    internal static string Clean(this string str)
    {
        return str.Replace("\n", " ").Replace('', ' ');
    }
}