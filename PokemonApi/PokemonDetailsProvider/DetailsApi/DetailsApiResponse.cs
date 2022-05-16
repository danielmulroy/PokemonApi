using Newtonsoft.Json;

namespace PokemonApi.PokemonDetailsProvider.DetailsApi;

public class DetailsApiResponse
{
    public string Name { get; set; }

    [JsonProperty("flavor_text_entries")]
    public IList<FlavorTextObj> FlavorTextList { get; set; }

    [JsonProperty("habitat")]
    public Habitat HabitatInternal { get; set; }

    [JsonProperty("is_legendary")]
    public bool IsLegendary { get; set; }

    [JsonIgnore]
    public string Description => FlavorTextList.FirstOrDefault(x => x.Language.Name.ToLower() == "en")?.Desc.Replace("\n", " ").Replace('', ' ');

    [JsonIgnore]
    public string Habitat => HabitatInternal.Name;
}

public class FlavorTextObj
{
    [JsonProperty("flavor_text")]
    public string Desc { get; set; }
    public Language Language { get; set; }
}

public class Language
{
    public string Name { get; set; }
}

public class Habitat
{
    public string Name { get; set; }
}