using System.Text.RegularExpressions;
using PokemonDetailsProvider.DetailsApi;
using PokemonDetailsProvider.DetailsProvider.Dto;

namespace PokemonDetailsProvider.DetailsProvider;

public class PokemonDetailsProvider : IPokemonDetailsProvider
{
    private readonly IDetailsApi _detailsApi;
    public PokemonDetailsProvider(IDetailsApi detailsApi)
    {
        _detailsApi = detailsApi;
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
        //Do translation
        return details;
    }

    public bool NameIsValid(string name, out string error)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            error = "Name is blank. Please provide a name in the request.";
            return false;
        }
        if (Regex.IsMatch(name, "[a-zA-Z]+"))
        {
            error = "Name contains non-letter characters. Please provide JUST the name in the request.";
            return false;
        }
        error = string.Empty;
        return true;
    }
}