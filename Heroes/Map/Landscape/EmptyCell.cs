using Heroes.Assets;
using Heroes.Map.Landscape;
using Point = System.Drawing.Point;

namespace Heroes.Map.Hex;

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