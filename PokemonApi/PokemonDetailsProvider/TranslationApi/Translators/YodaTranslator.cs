using PokemonApi.PokemonDetailsProvider.RequestWrapper;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

public class YodaTranslator : TranslatorBase
{
    public YodaTranslator(IConfiguration configuration, IRequestWrapper wrapper)
        : base(TranslatorType.Yoda, configuration, wrapper)
    {
    }
}