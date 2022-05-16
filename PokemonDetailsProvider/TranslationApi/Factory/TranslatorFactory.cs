using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonDetailsProvider.TranslationApi.Translators;

namespace PokemonDetailsProvider.TranslationApi.Factory
{
    public class TranslatorFactory : ITranslatorFactory
    {
        public ITranslator GetTranslator(PokemonDetails details)
        {
            if (details.IsLegendary || details.Habitat == "cave") 
                return new YodaTranslator();

            return new ShakespeareTranslator();
        }
    }
}
