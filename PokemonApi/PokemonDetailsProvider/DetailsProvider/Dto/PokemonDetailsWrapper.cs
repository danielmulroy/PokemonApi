using System.Net;

namespace PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;

public class PokemonDetailsWrapper
{
    public PokemonDetailsWrapper(PokemonDetails details)
    {
        Details = details;
    }
    public PokemonDetails Details { get; }

    public HttpStatusCode? Status { get; private set; } = HttpStatusCode.OK;

    public string ErrorMessage { get; private set; }

    public bool HasError => Status != HttpStatusCode.OK;

    public void AddError(HttpStatusCode? status, string message)
    {
        Status = status;
        ErrorMessage = message;
    }
}