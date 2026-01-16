using Heroes.Assets;
using Point = System.Drawing.Point;

namespace Heroes.Map.Rectangle;

public class RectangleCell : IMapItem
{
    public RectangleCell(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public Point Coordinates { get; set; }

    public string Name => "Empty";
}