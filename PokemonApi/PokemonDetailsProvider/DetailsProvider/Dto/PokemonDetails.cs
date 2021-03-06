namespace PokemonApi.PokemonDetailsProvider.DetailsProvider.Dto;

public class PokemonDetails
{
    public PokemonDetails(string name, string description, string habitat, bool isLegendary)
    {
        Name = name;
        Description = description;
        Habitat = habitat;
        IsLegendary = isLegendary;
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Habitat { get; set; }
    public bool IsLegendary { get; set; }
}