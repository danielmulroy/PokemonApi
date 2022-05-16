using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

public class ShakespeareTranslator : TranslatorBase
{
    public ShakespeareTranslator(IConfiguration configuration, IRequestWrapper wrapper) 
        : base(TranslatorType.Shakespeare, configuration, wrapper)
    {
    }
}