using Newtonsoft.Json;
using PokemonApi.PokemonDetailsProvider.ApiCache;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using RestSharp;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

public abstract class TranslatorBase : ITranslator
{
    private readonly RestClient _client;
    private readonly IRequestWrapper _wrapper;
    private readonly bool _cachingEnabled;
    private readonly LeastRecentlyUsedCache<string, string> _cache;
    private string _urlElement;

    protected TranslatorBase(TranslatorType type, IConfiguration configuration, IRequestWrapper wrapper)
    {
        var url = configuration.GetSection("ExternalApis").GetSection("TranslationUrl").Value;
        _client = new RestClient(url + (url.EndsWith('/') ? "" : "/"));
        _urlElement = type + ".json?text=";
        _wrapper = wrapper;

        try
        {
            _cachingEnabled = configuration.GetSection("Caching").GetValue<bool>("Enabled");
            if (!_cachingEnabled) return;

            var cacheCap = configuration.GetSection("Caching").GetValue<int>("Capacity");
            _cache = new LeastRecentlyUsedCache<string, string>(cacheCap);
        }
        catch
        {
            _cachingEnabled = false;
        }
    }

    public async Task<string> Translate(string text)
    {
        if (_cachingEnabled && _cache.TryGet(text, out var cache)) return cache;

        var request = new RestRequest(_urlElement + text, Method.Post);

        var response = await _wrapper.Get(_client, request);

        if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null) return text;

        var translated = JsonConvert.DeserializeObject<TranslationResponse>(response.Content).Contents.Translated;
        if (_cachingEnabled) _cache.Put(text, translated);
        return translated;
    }

    private record ReqBody(string text);

    public enum TranslatorType
    {
        Shakespeare,
        Yoda
    }
}
