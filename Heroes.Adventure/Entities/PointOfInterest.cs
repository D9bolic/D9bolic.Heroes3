using System.Drawing;

namespace Heroes.Adventure.Entities;

public class PointOfInterest : IAdventureEntity
{
    public PointOfInterest(string name, Point coordinates)
    {
        Name = name;
        Coordinates = coordinates;
    }

    public string Name { get; }

    public Point Coordinates { get; set; }

    public bool IsInteractable => true;

    public bool IsVisited { get; set; }
}
