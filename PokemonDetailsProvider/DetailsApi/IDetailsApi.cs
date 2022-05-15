using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonDetailsProvider.DetailsProvider.Dto;

namespace PokemonDetailsProvider.DetailsApi
{
    public interface IDetailsApi
    {
        Task<DetailsApiResponse> Get(string name);
    }
}
