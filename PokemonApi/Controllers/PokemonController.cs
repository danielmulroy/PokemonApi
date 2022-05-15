using System.Net;
using Microsoft.AspNetCore.Mvc;
using PokemonDetailsProvider.DetailsProvider;
using PokemonDetailsProvider.DetailsProvider.Dto;

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
        if (!_detailsProvider.NameIsValid(name, out var error)) return BadRequest(error);
        var res = await _detailsProvider.GetPokemonDetails(name);
        if (res == null) return BadRequest("Could not retrieve Pokemon details. Does this Pokemon exist?");
        return Ok(res);
    }

    [HttpGet("translated/{name}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PokemonDetails), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTranslated(string name)
    {
        if (!_detailsProvider.NameIsValid(name, out var error)) return BadRequest(error);
        var res = await _detailsProvider.GetTranslatedPokemonDetails(name);
        if (res == null) return BadRequest("Could not retrieve Pokemon details. Does this Pokemon exist?");
        return Ok(res);
    }
}