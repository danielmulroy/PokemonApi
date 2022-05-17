using System.Net;

namespace PokemonApi.ErrorHandling;

public class InvalidRequestException : HttpRequestException
{
    public InvalidRequestException(string message) : base(message, null, HttpStatusCode.BadRequest){}
}

public class InvalidResponseException : HttpRequestException
{
    public InvalidResponseException(string message) : base(message, null, HttpStatusCode.NotFound) { }
}