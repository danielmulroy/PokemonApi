using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonDetailsProvider.TranslationApi.Translators;

namespace PokemonDetailsProvider.TranslationApi.Factory
{
    public interface ITranslatorFactory
    {
        public ITranslator GetTranslator(PokemonDetails details);
    }
}
