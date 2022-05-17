using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonApi.Controllers;
using PokemonApi.PokemonDetailsProvider.DetailsProvider;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PokemonApi.Test;

public class ControllerTests
{
    [Fact]
    public async void GetNormal_ReturnsCorrectDataAndStatus_IfSuccessful()
    {
        var mockProvider = new Mock<IPokemonDetailsProvider>();
        
        mockProvider.Setup(m => m.GetPokemonDetails(It.IsAny<string>())).ReturnsAsync(GetGoodDetails(false));

        var controllerUnderTest = new PokemonController(mockProvider.Object);

        var result = await controllerUnderTest.GetNormal("Dave");

        Assert.Equal(typeof(OkObjectResult), result.GetType());
    }

    [Fact]
    public async void GetNormal_ReturnsCorrectDataAndStatus_IfUnsuccessful()
    {
        var mockProvider = new Mock<IPokemonDetailsProvider>();
        mockProvider.Setup(m => m.GetPokemonDetails(It.IsAny<string>())).ReturnsAsync(GetBadDetails);

        var controllerUnderTest = new PokemonController(mockProvider.Object);

        var result = await controllerUnderTest.GetNormal("Dave");

        Assert.Equal(typeof(ObjectResult), result.GetType());

        var or = (ObjectResult)result;
        Assert.Equal((int)HttpStatusCode.BadRequest, or.StatusCode);
    }

    [Fact]
    public async void GetTranslated_ReturnsCorrectDataAndStatus_IfSuccessful()
    {
        var mockProvider = new Mock<IPokemonDetailsProvider>();
        
        mockProvider.Setup(m => m.GetTranslatedPokemonDetails(It.IsAny<string>())).ReturnsAsync(GetGoodDetails(true));

        var controllerUnderTest = new PokemonController(mockProvider.Object);

        var result = await controllerUnderTest.GetTranslated("Dave");

        Assert.Equal(typeof(OkObjectResult), result.GetType());
    }

    [Fact]
    public async void GetTranslated_ReturnsCorrectDataAndStatus_IfUnsuccessful()
    {
        var mockProvider = new Mock<IPokemonDetailsProvider>();
        mockProvider.Setup(m => m.GetTranslatedPokemonDetails(It.IsAny<string>())).ReturnsAsync(GetBadDetails);

        var controllerUnderTest = new PokemonController(mockProvider.Object);

        var result = await controllerUnderTest.GetTranslated("Dave");

        Assert.Equal(typeof(ObjectResult), result.GetType());

        var or = (ObjectResult)result;
        Assert.Equal((int)HttpStatusCode.BadRequest, or.StatusCode);
    }

    private PokemonDetailsWrapper GetBadDetails()
    {
        var wrapper = GetGoodDetails(false);
        wrapper.AddError(HttpStatusCode.BadRequest, "BLAH BLAH");
        return wrapper;
    }

    private static PokemonDetailsWrapper GetGoodDetails(bool translated)
    {
        var details = new PokemonDetails("Dave", "My name is Dave", "DaveHouse", true);
        if (translated) details.Description = "Me llamo Dave";
        return new PokemonDetailsWrapper(details);
    }
}