using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;

public interface ITranslatorFactory
{
    public ITranslator GetTranslator(PokemonDetails details);
}