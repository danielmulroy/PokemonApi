using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PokemonDetailsProvider.RequestWrapper;
using RestSharp;

namespace PokemonDetailsProvider.TranslationApi.Translators;

public abstract class TranslatorBase
{
    private readonly RestClient _client;
    private readonly IRequestWrapper _wrapper;
    protected readonly TranslatorType _type;

    public TranslatorBase(TranslatorType type, IConfiguration configuration, IRequestWrapper wrapper)
    {
        var baseUrl = configuration.GetSection("ExternalApis").GetSection($"{_type}Url").Value;
        _client = new RestClient(baseUrl + (baseUrl.EndsWith('/') ? "" : "/") + "{name}");
        _wrapper = wrapper;
    }

    public string Translate(string text)
    {
        
    }

    public enum TranslatorType
    {
        Shakespeare,
        Yoda
    }
}
