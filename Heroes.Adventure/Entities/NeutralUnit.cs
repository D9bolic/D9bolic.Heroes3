using System.Drawing;

namespace Heroes.Adventure.Entities;

public class NeutralUnit : IAdventureEntity
{
    public NeutralUnit(string name, Point coordinates, int count)
    {
        Name = name;
        Coordinates = coordinates;
        Count = count;
    }

    public string Name { get; }

    public Point Coordinates { get; set; }

    public bool IsInteractable => true;

    public int Count { get; }
}
