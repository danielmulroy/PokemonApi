using RestSharp;

namespace PokemonApi.PokemonDetailsProvider.RequestWrapper;

public interface IRequestWrapper
{
    Task<RestResponse> Get(RestClient client, RestRequest request);
}