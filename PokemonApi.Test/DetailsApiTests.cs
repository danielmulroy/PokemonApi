using Microsoft.Extensions.Configuration;
using Moq;
using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using RestSharp;
using System.Linq;
using System.Net;
using Xunit;

namespace PokemonApi.Test;

public class DetailsApiTests
{
    private IDetailsApi _apiUnderTest;
    private readonly Mock<IRequestWrapper> _requestWrapper;
    private readonly Mock<IConfiguration> _configuration;

    public DetailsApiTests()
    {
        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(m => m.Value).Returns("localhost:5050/blahblah");
        mockSection.Setup(m => m.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
        _configuration = new Mock<IConfiguration>();
        _configuration.Setup(m => m.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
        _requestWrapper = new Mock<IRequestWrapper>();
    }

    [Fact]
    public async void GetDetails_WorksIfPokemonExists()
    {
        _requestWrapper.Setup(m => m.Get(It.IsAny<RestClient>(), It.IsAny<RestRequest>()))
            .ReturnsAsync(GetGoodResponse());

        _apiUnderTest = new DetailsApi(_configuration.Object, _requestWrapper.Object);

        var resp = await _apiUnderTest.Get("Mewtwo");

        Assert.NotNull(resp);
        Assert.Equal(resp.FlavorTextList.FirstOrDefault().Desc.Clean(), resp.Description);
        Assert.Equal("en", resp.FlavorTextList.FirstOrDefault().Language.Name);
        Assert.Equal(resp.HabitatInternal.Name, resp.Habitat);
        Assert.True(resp.IsLegendary);
        Assert.Equal("mewtwo", resp.Name);
    }

    [Fact]
    public async void GetDetails_ReturnsNullIfPokemonIsNotFound()
    {
        _requestWrapper.Setup(m => m.Get(It.IsAny<RestClient>(), It.IsAny<RestRequest>()))
            .ReturnsAsync(GetBadResponse());

        _apiUnderTest = new DetailsApi(_configuration.Object, _requestWrapper.Object);

        var resp = await _apiUnderTest.Get("Diglett");

        Assert.Null(resp);
    }

    private static RestResponse GetGoodResponse()
    {
        var response = new MockRestResponse(HttpStatusCode.OK, ExampleResponses.Mewtwo);

        return response;
    }

    private static RestResponse GetBadResponse()
    {
        var response = new MockRestResponse(HttpStatusCode.NotFound, "Not found");

        return response;
    }


}