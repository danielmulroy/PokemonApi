using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using Microsoft.Extensions.Configuration;
using Moq;
using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;
using RestSharp;
using Xunit;

namespace PokemonApi.Test;

public class TranslationApiTests
{
    private TranslatorBase _apiUnderTest;
    private Mock<IRequestWrapper> _requestWrapper;
    private Mock<IConfiguration> _configuration;

    public TranslationApiTests()
    {
        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(m => m.Value).Returns("localhost:5050/blahblah");
        mockSection.Setup(m => m.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
        _configuration = new Mock<IConfiguration>();
        _configuration.Setup(m => m.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
        _requestWrapper = new Mock<IRequestWrapper>();
    }

    [Fact]
    public async void YodaTranslation_ReturnsCorrectTranslation_WhenResponseIsGood()
    {
        _requestWrapper.Setup(m => m.Get(It.IsAny<RestClient>(), It.IsAny<RestRequest>()))
            .ReturnsAsync(GetGoodYodaResponse);

        _apiUnderTest = new YodaTranslator(_configuration.Object, _requestWrapper.Object);

        var text = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
        var resp = await _apiUnderTest.Translate(text);

        Assert.NotNull(resp);
        Assert.NotEqual(text, resp);
    }

    [Fact]
    public async void ShakespeareTranslation_ReturnsCorrectTranslation_WhenResponseIsGood()
    {
        _requestWrapper.Setup(m => m.Get(It.IsAny<RestClient>(), It.IsAny<RestRequest>()))
            .ReturnsAsync(GetGoodShakespeareResponse);

        _apiUnderTest = new ShakespeareTranslator(_configuration.Object, _requestWrapper.Object);

        var text = "Spits fire that is hot enough to melt boulders.Known to cause forest fires unintentionally.";
        var resp = await _apiUnderTest.Translate(text);

        Assert.NotNull(resp);
        Assert.NotEqual(text, resp);
    }

    [Fact]
    public async void YodaTranslation_ReturnsNoTranslation_WhenResponseIsBad()
    {
        _requestWrapper.Setup(m => m.Get(It.IsAny<RestClient>(), It.IsAny<RestRequest>()))
            .ReturnsAsync(GetBadResponse);

        _apiUnderTest = new YodaTranslator(_configuration.Object, _requestWrapper.Object);

        var text = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
        var resp = await _apiUnderTest.Translate(text);

        Assert.NotNull(resp);
        Assert.Equal(text, resp);
    }

    [Fact]
    public async void ShakespeareTranslation_ReturnsNoTranslation_WhenResponseIsBad()
    {
        _requestWrapper.Setup(m => m.Get(It.IsAny<RestClient>(), It.IsAny<RestRequest>()))
            .ReturnsAsync(GetBadResponse);

        _apiUnderTest = new ShakespeareTranslator(_configuration.Object, _requestWrapper.Object);

        var text = "Spits fire that is hot enough to melt boulders.Known to cause forest fires unintentionally.";
        var resp = await _apiUnderTest.Translate(text);

        Assert.NotNull(resp);
        Assert.Equal(text, resp);
    }

    private static RestResponse GetGoodYodaResponse()
    {
        var response = new MockRestResponse(HttpStatusCode.OK, ExampleResponses.MewtwoYoda);

        return response;
    }

    private static RestResponse GetGoodShakespeareResponse()
    {
        var response = new MockRestResponse(HttpStatusCode.OK, ExampleResponses.CharizardShakespeare);

        return response;
    }

    private static RestResponse GetBadResponse()
    {
        var response = new MockRestResponse(HttpStatusCode.BadRequest, "Missing Part");

        return response;
    }

    
}