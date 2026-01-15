using System.Drawing;

namespace Heroes.Map.Landscape;

public class Rock : IMapItem
{
    public Rock(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; set; }
    
    public string Name => "Rock";
}