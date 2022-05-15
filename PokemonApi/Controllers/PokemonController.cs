using Microsoft.AspNetCore.Mvc;

namespace PokemonApi.Controllers;

[Route("pokemon")]
[ApiController]
public class PokemonController : ControllerBase
{

    [HttpGet("{name}")]
    public async Task<IActionResult> GetNormal(string name)
    {
        throw new NotImplementedException();
    }

    [HttpGet("translated/{name}")]
    public async Task<IActionResult> GetTranslated(string name)
    {
        throw new NotImplementedException();
    }
}