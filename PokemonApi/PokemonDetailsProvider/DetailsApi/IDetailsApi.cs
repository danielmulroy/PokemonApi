namespace PokemonApi.PokemonDetailsProvider.DetailsApi;

public interface IDetailsApi
{
    public Task<DetailsApiResponse> Get(string name);
}