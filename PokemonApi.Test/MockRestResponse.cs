using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PokemonApi.Test;

public class MockRestResponse : RestResponse
{
    public MockRestResponse(HttpStatusCode status, string content)
    {
        StatusCode = status;
        Content = content;
    }
}