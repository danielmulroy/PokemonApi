using Microsoft.AspNetCore.Mvc;
using Moq;
using PokemonApi.Controllers;
using PokemonApi.PokemonDetailsProvider.DetailsProvider;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using System;
using Xunit;

namespace PokemonApi.Test;

public class ControllerTests
{
    [Theory]
    [InlineData(true, true, typeof(BadRequestObjectResult))]
    [InlineData(true, false, typeof(OkObjectResult))]
    [InlineData(false, true, typeof(BadRequestObjectResult))]
    [InlineData(false, false, typeof(BadRequestObjectResult))]
    public async void GetNormal_ReturnsCorrectDataAndStatus(bool validName, bool nullDetails, Type expectedType)
    {
        var mockProvider = new Mock<IPokemonDetailsProvider>();
        var err = "";
        mockProvider.Setup(m => m.NameIsValid(It.IsAny<string>(), out err)).Returns(validName);
        mockProvider.Setup(m => m.GetPokemonDetails(It.IsAny<string>())).ReturnsAsync(GetDetails(false, nullDetails));

        var controllerUnderTest = new PokemonController(mockProvider.Object);

        var result = await controllerUnderTest.GetNormal("Dave");

        Assert.Equal(expectedType, result.GetType());
    }

    [Theory]
    [InlineData(true, true, typeof(BadRequestObjectResult))]
    [InlineData(true, false, typeof(OkObjectResult))]
    [InlineData(false, true, typeof(BadRequestObjectResult))]
    [InlineData(false, false, typeof(BadRequestObjectResult))]
    public async void GetTranslated_ReturnsCorrectDataAndStatus(bool validName, bool nullDetails, Type expectedType)
    {
        var mockProvider = new Mock<IPokemonDetailsProvider>();
        var err = "";
        mockProvider.Setup(m => m.NameIsValid(It.IsAny<string>(), out err)).Returns(validName);
        mockProvider.Setup(m => m.GetTranslatedPokemonDetails(It.IsAny<string>())).ReturnsAsync(GetDetails(true, nullDetails));

        var controllerUnderTest = new PokemonController(mockProvider.Object);

        var result = await controllerUnderTest.GetTranslated("Dave");

        Assert.Equal(expectedType, result.GetType());
    }


    private static PokemonDetails GetDetails(bool translated, bool nullDetails)
    {
        if (nullDetails) return null;

        var details = new PokemonDetails("Dave", "My name is Dave", "DaveHouse", true);
        if (translated) details.Description = "Me llamo Dave";
        return details;
    }
}