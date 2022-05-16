namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

public interface ITranslator
{
    Task<string> Translate(string text);
}