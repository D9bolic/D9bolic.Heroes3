using System.Drawing;
using Heroes.Adventure.Players;

namespace Heroes.Adventure.Entities.Resources;

public class ResourceSite : IAdventureEntity, IOwnable
{
    public ResourceSite(string name, Point coordinates, ResourceType resourceType, int incomePerDay)
    {
        Name = name;
        Coordinates = coordinates;
        ResourceType = resourceType;
        IncomePerDay = incomePerDay;
    }

    public string Name { get; }

    public Point Coordinates { get; set; }

    public bool IsInteractable => true;

    public ResourceType ResourceType { get; }

    public int IncomePerDay { get; }

    public AdventurePlayer? Owner { get; set; }
}
