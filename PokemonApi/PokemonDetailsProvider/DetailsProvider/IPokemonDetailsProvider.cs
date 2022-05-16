using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;

namespace PokemonApi.PokemonDetailsProvider.DetailsProvider;

/// <summary>
/// This is the only public interface from the DetailsProvider.
/// No external process should be able to augment this code library
/// my any means other than via this interface.
/// </summary>
public interface IPokemonDetailsProvider
{
    public Task<PokemonDetails> GetPokemonDetails(string name);
    public Task<PokemonDetails> GetTranslatedPokemonDetails(string name);
    bool NameIsValid(string name, out string error);
}