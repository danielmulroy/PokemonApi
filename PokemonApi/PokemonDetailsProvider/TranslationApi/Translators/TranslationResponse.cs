namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

internal class TranslationResponse
{
    public Content Contents { get; set; }
}

internal class Content
{
    public string Translated { get; set; }
}