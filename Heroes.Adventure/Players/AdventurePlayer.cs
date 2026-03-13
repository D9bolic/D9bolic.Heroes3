using Heroes.Adventure.Entities.Heroes;
using Heroes.Adventure.Entities.Resources;

namespace Heroes.Adventure.Players;

public class AdventurePlayer
{
    public string Name { get; init; } = string.Empty;

    public List<AdventureHero> Heroes { get; init; } = [];

    public Dictionary<ResourceType, int> Resources { get; init; } = new()
    {
        { ResourceType.Gold, 0 },
        { ResourceType.Wood, 0 },
        { ResourceType.Ore, 0 },
        { ResourceType.Mercury, 0 },
        { ResourceType.Sulfur, 0 },
        { ResourceType.Crystal, 0 },
        { ResourceType.Gems, 0 },
    };
}
