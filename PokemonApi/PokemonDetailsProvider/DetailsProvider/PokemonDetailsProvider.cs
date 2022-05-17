using System.Net;
using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;
using System.Text.RegularExpressions;
using PokemonApi.ErrorHandling;

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

    public async Task<PokemonDetailsWrapper> GetPokemonDetails(string name)
    {
        PokemonDetailsWrapper wrapper = default;
        
        try
        {
            ValidateName(name);
            var apiDetails = await _detailsApi.Get(name);
            wrapper = new PokemonDetailsWrapper(new PokemonDetails(apiDetails.Name, apiDetails.Description, apiDetails.Habitat, apiDetails.IsLegendary));
        }
        catch (HttpRequestException ex)
        {
            wrapper ??= new PokemonDetailsWrapper(null);
            wrapper.AddError(ex.StatusCode, ex.Message);
        }

        return wrapper;
    }

    public async Task<PokemonDetailsWrapper> GetTranslatedPokemonDetails(string name)
    {
        var wrapper = await GetPokemonDetails(name);
        if (wrapper.HasError) return wrapper;

        try
        {
            wrapper.Details.Description =
                await _factory.GetTranslator(wrapper.Details).Translate(wrapper.Details.Description);
        }
        catch (HttpRequestException ex)
        {
            wrapper.AddError(ex.StatusCode, ex.Message);
        }

        return wrapper;
    }

    public void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidRequestException("Name is blank. Please provide a name in the request.");
        if (!Regex.IsMatch(name, "^[A-Za-z]+$"))
            throw new InvalidRequestException("Name contains non-letter characters. Please provide JUST the name in the request.");
    }
}