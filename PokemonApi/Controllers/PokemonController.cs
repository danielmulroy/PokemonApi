using Microsoft.AspNetCore.Mvc;
using PokemonApi.PokemonDetailsProvider.DetailsProvider;
using PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;
using System.Net;

namespace PokemonApi.Controllers;

[Route("pokemon")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonDetailsProvider _detailsProvider;

    public PokemonController(IPokemonDetailsProvider detailsProvider)
    {
        _detailsProvider = detailsProvider ?? throw new ArgumentNullException(nameof(detailsProvider));
    }

    [HttpGet("{name}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PokemonDetails), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetNormal(string name)
    {
        var res = await _detailsProvider.GetPokemonDetails(name);
        return res.HasError ? new ObjectResult(res.ErrorMessage) { StatusCode = (int)res.Status } : Ok(res.Details);
    }

    [HttpGet("translated/{name}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PokemonDetails), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTranslated(string name)
    {
        var res = await _detailsProvider.GetTranslatedPokemonDetails(name);
        return res.HasError ? new ObjectResult(res.ErrorMessage) { StatusCode = (int)res.Status } : Ok(res.Details);
    }
}