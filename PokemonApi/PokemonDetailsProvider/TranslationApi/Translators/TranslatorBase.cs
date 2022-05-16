using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using RestSharp;

namespace PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;

public abstract class TranslatorBase
{
    private readonly RestClient _client;
    private readonly IRequestWrapper _wrapper;

    public TranslatorBase(TranslatorType type, IConfiguration configuration, IRequestWrapper wrapper)
    {
        var url = configuration.GetSection("ExternalApis").GetSection($"TranslationUrl").Value;
        _client = new RestClient(url + (url.EndsWith('/') ? "" : "/") + type);
        _wrapper = wrapper;
    }

    public async Task<string> Translate(string text)
    {
        var request = new RestRequest();
        request.AddJsonBody(new ReqBody(text));

        var response = await _wrapper.Get(_client, request);

        if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null) return text;

        return JsonConvert.DeserializeObject<TranslationResponse>(response.Content).Contents.Translated;
    }

    private record ReqBody(string text);

    public enum TranslatorType
    {
        Shakespeare,
        Yoda
    }
}
