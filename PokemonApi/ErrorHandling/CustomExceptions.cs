using System.Net;

namespace PokemonApi.ErrorHandling;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode;

    public HttpException(HttpStatusCode status, string message) : base(message)
    {
        StatusCode = status;
    }
}

public class InvalidRequestException : HttpRequestException
{
    public InvalidRequestException(string message) : base(message, null, HttpStatusCode.BadRequest){}
}

public class InvalidResponseException : HttpRequestException
{
    public InvalidResponseException(string message) : base(message, null, HttpStatusCode.NotFound) { }
}