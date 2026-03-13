using System.Drawing;
using Heroes.Adventure.Players;

namespace Heroes.Adventure.Entities.Cities;

public class City : IAdventureEntity, IOwnable
{
    public City(string name, Point coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }

    public string Name { get; }

    public Point Coordinates { get; set; }

    public bool IsInteractable => true;

    public AdventurePlayer? Owner { get; set; }

    public List<Building> Buildings { get; init; } = [];

    public List<GarrisonSlot> Garrison { get; init; } = [];

    public List<RecruitmentPool> RecruitmentPools { get; init; } = [];
}
