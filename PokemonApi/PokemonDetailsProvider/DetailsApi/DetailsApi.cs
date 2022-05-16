using Newtonsoft.Json;
using PokemonApi.PokemonDetailsProvider.ApiCache;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using RestSharp;

namespace PokemonApi.PokemonDetailsProvider.DetailsApi;

public class DetailsApi : IDetailsApi
{
    private readonly RestClient _client;
    private readonly IRequestWrapper _wrapper;
    private readonly bool _cachingEnabled;
    private readonly LeastRecentlyUsedCache<string, DetailsApiResponse> _cache;

    public DetailsApi(IConfiguration configuration, IRequestWrapper wrapper)
    {
        var url = configuration.GetSection("ExternalApis").GetSection("PokemonDetailsUrl").Value;
        _client = new RestClient(url + (url.EndsWith('/') ? "" : "/") + "{name}");
        _wrapper = wrapper;
        try
        {
            _cachingEnabled = configuration.GetSection("Caching").GetValue<bool>("Enabled");
            if (!_cachingEnabled) return;

            var cacheCap = configuration.GetSection("Caching").GetValue<int>("Capacity");
            _cache = new LeastRecentlyUsedCache<string, DetailsApiResponse>(cacheCap);
        }
        catch
        {
            _cachingEnabled = false;
        }
    }

    public async Task<DetailsApiResponse> Get(string name)
    {
        if (_cachingEnabled && _cache.TryGet(name, out var cache)) return cache;

        var request = new RestRequest();
        request.AddUrlSegment("name", name);

        var response = await _wrapper.Get(_client, request);

        if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null) return null;

        var details = JsonConvert.DeserializeObject<DetailsApiResponse>(response.Content);
        if (_cachingEnabled) _cache.Put(name, details);
        return details;
    }
}