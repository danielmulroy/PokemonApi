using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.PokemonDetailsProvider.DetailsApi;

public interface IDetailsApi
{
    public Task<DetailsApiResponse> Get(string name);
}