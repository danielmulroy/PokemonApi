using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PokemonApi.PokemonDetailsProvider.RequestWrapper;

public interface IRequestWrapper
{
    Task<RestResponse> Get(RestClient client, RestRequest request);
}