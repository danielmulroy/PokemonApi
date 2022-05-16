using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;

public interface ITranslatorFactory
{
    public ITranslator GetTranslator(PokemonDetails details);
}