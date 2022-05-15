using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PokemonDetailsProvider.DetailsProvider.Dto;
using PokemonDetailsProvider.RequestWrapper;
using RestSharp;

namespace PokemonDetailsProvider.DetailsApi
{
    public class DetailsApi : IDetailsApi
    {
        private readonly RestClient _client;
        private readonly IRequestWrapper _wrapper;

        public DetailsApi(IConfiguration configuration, IRequestWrapper wrapper)
        {
            var baseUrl = configuration.GetSection("ExternalApis").GetSection("PokemonDetailsUrl").Value;
            _client = new RestClient(baseUrl + (baseUrl.EndsWith('/') ? "" : "/") + "{name}");
            _wrapper = wrapper;
        }

        public async Task<DetailsApiResponse> Get(string name)
        {
            var request = new RestRequest();
            request.AddUrlSegment("name", name);

            var response = await _wrapper.Get(_client, request);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null) return null;

            return JsonConvert.DeserializeObject<DetailsApiResponse>(response.Content);
        }
    }
}
