using RestSharp;
using System.Net;

namespace PokemonApi.Test;

public class MockRestResponse : RestResponse
{
    public MockRestResponse(HttpStatusCode status, string content)
    {
        StatusCode = status;
        Content = content;
    }
}