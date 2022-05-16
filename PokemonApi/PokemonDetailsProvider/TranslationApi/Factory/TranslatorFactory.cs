using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;

public class TranslatorFactory : ITranslatorFactory
{
    private readonly IConfiguration _configuration;
    private readonly IRequestWrapper _wrapper;

    public TranslatorFactory(IConfiguration configuration, IRequestWrapper wrapper)
    {
        _configuration = configuration;
        _wrapper = wrapper;
    }

    public TranslatorBase GetTranslator(PokemonDetails details)
    {
        if (details.IsLegendary || details.Habitat == "cave")
            return new YodaTranslator(_configuration, _wrapper);

        return new ShakespeareTranslator(_configuration, _wrapper);
    }
}