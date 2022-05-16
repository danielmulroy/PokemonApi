using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;
using System.Text.RegularExpressions;

namespace PokemonApi.PokemonDetailsProvider.DetailsProvider;

public class PokemonDetailsProvider : IPokemonDetailsProvider
{
    private readonly IDetailsApi _detailsApi;
    private readonly ITranslatorFactory _factory;

    public PokemonDetailsProvider(IDetailsApi detailsApi, ITranslatorFactory factory)
    {
        _detailsApi = detailsApi;
        _factory = factory;
    }

    public async Task<PokemonDetails> GetPokemonDetails(string name)
    {
        var apiDetails = await _detailsApi.Get(name);
        if (apiDetails == null) return null;
        var details = new PokemonDetails(apiDetails.Name, apiDetails.Description, apiDetails.Habitat, apiDetails.IsLegendary);
        return details;
    }

    public async Task<PokemonDetails> GetTranslatedPokemonDetails(string name)
    {
        var details = await GetPokemonDetails(name);
        details.Description = await _factory.GetTranslator(details).Translate(details.Description);
        return details;
    }

    public bool NameIsValid(string name, out string error)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            error = "Name is blank. Please provide a name in the request.";
            return false;
        }
        if (!Regex.IsMatch(name, "^[A-Za-z]+$"))
        {
            error = "Name contains non-letter characters. Please provide JUST the name in the request.";
            return false;
        }
        error = string.Empty;
        return true;
    }
}