using Heroes.Adventure.Entities.Resources;

namespace Heroes.Adventure.Entities.Cities;

public class Building
{
    public string Name { get; init; } = string.Empty;

    public bool IsBuilt { get; set; }

    public Dictionary<ResourceType, int> Cost { get; init; } = [];
}
