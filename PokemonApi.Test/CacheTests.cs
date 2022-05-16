using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PokemonApi.Controllers;
using PokemonApi.PokemonDetailsProvider.ApiCache;
using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.DetailsProvider;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;
using RestSharp;
using Xunit;

namespace PokemonApi.Test;

public class CacheTests
{
    private LeastRecentlyUsedCache<int, string> _cacheUnderTest;

    [Fact]
    public void CacheWhenFull_RemovesOldest()
    {
        _cacheUnderTest = new LeastRecentlyUsedCache<int, string>(2);

        _cacheUnderTest.Put(1, "One");
        _cacheUnderTest.Put(2, "Two");
        _cacheUnderTest.Put(3, "Three");

        Assert.True(_cacheUnderTest.TryGet(3, out _));
        Assert.True(_cacheUnderTest.TryGet(2, out _));
        Assert.False(_cacheUnderTest.TryGet(1, out _));
    }

    [Fact]
    public void CacheOverwrites_IfExists()
    {
        _cacheUnderTest = new LeastRecentlyUsedCache<int, string>(1);

        _cacheUnderTest.Put(1, "One");
        var oneButFancy = "JustTheSingleOfAGivenUnit";
        _cacheUnderTest.Put(1, oneButFancy);

        _cacheUnderTest.TryGet(1, out var one);
        Assert.Equal(oneButFancy, one);
    }

}