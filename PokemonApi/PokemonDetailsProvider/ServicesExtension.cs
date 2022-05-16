using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;

namespace PokemonApi.PokemonDetailsProvider;

public static class ServicesExtension
{
    public static IServiceCollection AddPokemonDetailsProviderDependencies(this IServiceCollection collection)
    {
        collection.AddSingleton<IRequestWrapper, RequestWrapper.RequestWrapper>();
        collection.AddSingleton<ITranslatorFactory, TranslatorFactory>();

        return collection;
    }
}