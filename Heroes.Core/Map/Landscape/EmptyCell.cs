using System.Drawing;

namespace Heroes.Map.Landscape;

public class EmptyCell : ILandscape
{
    public EmptyCell(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; set; }

    public string Name => "Empty";

    public bool CanMoveInto => true;
}
