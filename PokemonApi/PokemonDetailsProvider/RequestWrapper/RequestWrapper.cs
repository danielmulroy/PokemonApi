using RestSharp;

namespace PokemonApi.PokemonDetailsProvider.RequestWrapper;

public class RequestWrapper : IRequestWrapper
{
    public async Task<RestResponse> Get(RestClient client, RestRequest request) => await client.GetAsync(request).ConfigureAwait(false);
}