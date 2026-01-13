using System.Drawing;

namespace Heroes.Map.Landscape;

public class Rock : IMapItem
{
    public Rock(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; }
    
    public string Name => "Landscape:Rock";
}