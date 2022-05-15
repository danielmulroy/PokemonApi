using RestSharp;

namespace PokemonDetailsProvider.RequestWrapper;

public class RequestWrapper : IRequestWrapper
{
    public async Task<RestResponse> Get(RestClient client, RestRequest request) => await client.GetAsync(request);
}