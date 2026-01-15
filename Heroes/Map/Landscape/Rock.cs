using System.Drawing;

namespace Heroes.Map.Landscape;

public class Rock : ILandscape
{
    public Rock(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; set; }
    
    public string Name => "Rock";

    public bool CanMoveInto => false;
}