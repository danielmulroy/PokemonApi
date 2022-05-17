using Moq;
using PokemonApi.PokemonDetailsProvider.DetailsApi;
using PokemonApi.PokemonDetailsProvider.DetailsProvider;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;
using System.Collections.Generic;
using Xunit;

namespace PokemonApi.Test;

public class DetailsProviderTests
{
    private readonly IPokemonDetailsProvider _providerUnderTest;

    private const string DESC = "My name is Dave";
    private const string TRANSLATED_DESC = "Me llamo Dave";

    public DetailsProviderTests()
    {
        var mockApi = new Mock<IDetailsApi>();
        var dave = new DetailsApiResponse
        {
            Name = "Dave",
            HabitatInternal = new Habitat { Name = "DaveHouse" },
            FlavorTextList = new List<FlavorTextObj>
                { new() { Desc = DESC, Language = new Language { Name = "en" } } },
            IsLegendary = true
        };
        mockApi.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync(dave);

        var mockFactory = new Mock<ITranslatorFactory>();
        var mockTranslator = new Mock<ITranslator>();
        mockTranslator.Setup(m => m.Translate(It.IsAny<string>())).ReturnsAsync(TRANSLATED_DESC);
        mockFactory.Setup(m => m.GetTranslator(It.IsAny<PokemonDetails>())).Returns(mockTranslator.Object);

        _providerUnderTest = new PokemonDetailsProvider.DetailsProvider.PokemonDetailsProvider(mockApi.Object, mockFactory.Object);
    }

    [Theory]
    [InlineData("Mewtwo", false)]
    [InlineData("Mew Two", true)]
    [InlineData("Mew2", true)]
    [InlineData("", true)]
    public async void GetPokemonDetails_ReturnsError_IfNameInvalid(string name, bool expected)
    {
        var resp = await _providerUnderTest.GetPokemonDetails(name);

        Assert.Equal(expected, resp.HasError);
    }

    [Fact]
    public async void GetPokemonDetails_ReturnsDetailsCorrectly()
    {
        var name = "Dave";
        var wrapper = await _providerUnderTest.GetPokemonDetails(name);

        Assert.Equal(name, wrapper.Details.Name);
        Assert.Equal(DESC, wrapper.Details.Description);
    }

    [Fact]
    public async void GetTranslatedPokemonDetails_ReturnsDetailsCorrectly()
    {
        var name = "Dave";
        var wrapper = await _providerUnderTest.GetTranslatedPokemonDetails(name);

        Assert.Equal(name, wrapper.Details.Name);
        Assert.Equal(TRANSLATED_DESC, wrapper.Details.Description);
    }
}