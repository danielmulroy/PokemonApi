using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonApi.PokemonDetailsProvider.RequestWrapper;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Factory;
using PokemonApi.PokemonDetailsProvider.TranslationApi.Translators;
using Xunit;

namespace PokemonApi.Test;

public class TranslatorFactoryTests
{
    private readonly ITranslatorFactory _factoryUnderTest;

    public TranslatorFactoryTests()
    {
        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(m => m.Value).Returns("localhost:5050/blahblah");
        mockSection.Setup(m => m.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
        var config = new Mock<IConfiguration>();
        config.Setup(m => m.GetSection(It.IsAny<string>())).Returns(mockSection.Object);

        _factoryUnderTest =
            new TranslatorFactory(config.Object, new Mock<IRequestWrapper>().Object);
    }

    [Theory]
    [InlineData("cave", true, typeof(YodaTranslator))]
    [InlineData("cave", false, typeof(YodaTranslator))]
    [InlineData("mountain", true, typeof(YodaTranslator))]
    [InlineData("mountain", false, typeof(ShakespeareTranslator))]
    public void TranslatorFactory_ShouldReturn_CorrectType(string habitat, bool legendary, Type type)
    {
        var details = new PokemonDetails("DAVE", "DESC", habitat, legendary);

        var translator = _factoryUnderTest.GetTranslator(details);

        Assert.NotNull(translator);
        Assert.IsType(type, translator);
    }
}