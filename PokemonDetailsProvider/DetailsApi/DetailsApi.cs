using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PokemonDetailsProvider.DetailsProvider.Dto;
using RestSharp;

namespace PokemonDetailsProvider.DetailsApi
{
    internal class DetailsApi : IDetailsApi
    {
        private readonly RestClient _client;

        public DetailsApi(IConfiguration configuration)
        {
            var baseUrl = configuration.GetSection("PokemonDetailsUrl").Value;
            _client = new RestClient(baseUrl + (baseUrl.EndsWith('/') ? "" : "/") + "{name}");
        }

        public async Task<DetailsApiResponse> Get(string name)
        {
            var request = new RestRequest();
            request.AddUrlSegment("name", name);

            var response = await _client.GetAsync(request);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Content == null) return null;

            return JsonConvert.DeserializeObject<DetailsApiResponse>(response.Content);
        }
    }
}
