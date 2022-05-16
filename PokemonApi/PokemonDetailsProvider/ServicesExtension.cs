using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.DetailsProvider;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;

namespace PokemonApi.PokemonDetailsProvider;

public static class ServicesExtension
{
    public static IServiceCollection AddPokemonDetailsProviderDependencies(this IServiceCollection collection)
    {
        collection.AddSingleton<IPokemonDetailsProvider, DetailsProvider.PokemonDetailsProvider>();
        collection.AddSingleton<IDetailsApi, DetailsApi.DetailsApi>();
        collection.AddSingleton<IRequestWrapper, RequestWrapper.RequestWrapper>();
        collection.AddSingleton<ITranslatorFactory, TranslatorFactory>();

        return collection;
    }
}